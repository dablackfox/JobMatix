-- JobMatix POS schema extensions
-- Adds columns to existing tables to preserve full fidelity of legacy MSSQL data
-- (customer/supplier/stock/invoice/invoice_lines/payments were originally simplified
-- versions of the legacy schema; these ALTERs restore the fields that were dropped),
-- and creates new tables for legacy concepts that had no Postgres equivalent yet
-- (goods received, purchase orders, sales orders, cashup, layby, subscriptions,
-- stocktake, supplier returns, serial audit trail, supplier codes, categories).
-- Run against: jobmatix_pos

BEGIN;

-- ==================== Extend existing tables ====================

ALTER TABLE customer
  ADD COLUMN IF NOT EXISTS firstname VARCHAR(100) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS lastname VARCHAR(100) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS title VARCHAR(20) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS opened_staff_id INTEGER,
  ADD COLUMN IF NOT EXISTS opened_staff_name VARCHAR(100) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS credit_days INTEGER NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS country VARCHAR(40) NOT NULL DEFAULT 'Australia',
  ADD COLUMN IF NOT EXISTS do_not_email_documents BOOLEAN NOT NULL DEFAULT false,
  ADD COLUMN IF NOT EXISTS tags VARCHAR(2000) NOT NULL DEFAULT '';

ALTER TABLE supplier
  ADD COLUMN IF NOT EXISTS alt_contact_name VARCHAR(100) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS alt_contact_position VARCHAR(100) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS alt_phone VARCHAR(40) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS alt_fax VARCHAR(40) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS alt_email VARCHAR(500) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS freight_free BOOLEAN NOT NULL DEFAULT false,
  ADD COLUMN IF NOT EXISTS reject_backorders BOOLEAN NOT NULL DEFAULT false,
  ADD COLUMN IF NOT EXISTS delivery_days INTEGER NOT NULL DEFAULT -1;

ALTER TABLE stock
  ADD COLUMN IF NOT EXISTS cat2 VARCHAR(12) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS model_no VARCHAR(80) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS sales_prompt VARCHAR(100) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS is_non_stock_item BOOLEAN NOT NULL DEFAULT false,
  ADD COLUMN IF NOT EXISTS allow_renaming BOOLEAN NOT NULL DEFAULT false,
  ADD COLUMN IF NOT EXISTS long_description TEXT NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS brand_name VARCHAR(50) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS goods_tax_code VARCHAR(14) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS cost_account VARCHAR(100) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS income_account VARCHAR(100) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS freight BOOLEAN NOT NULL DEFAULT false;

ALTER TABLE invoice
  ADD COLUMN IF NOT EXISTS is_on_account BOOLEAN NOT NULL DEFAULT false,
  ADD COLUMN IF NOT EXISTS payment_id INTEGER,
  ADD COLUMN IF NOT EXISTS job_number INTEGER,
  ADD COLUMN IF NOT EXISTS delivered_layby_id INTEGER,
  ADD COLUMN IF NOT EXISTS original_id INTEGER,
  ADD COLUMN IF NOT EXISTS terminal_id VARCHAR(300),
  ADD COLUMN IF NOT EXISTS cash_drawer VARCHAR(30) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS current_windows_username VARCHAR(160) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS discount_nett DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS discount_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS rounding DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS delivery_instructions TEXT NOT NULL DEFAULT '';

ALTER TABLE invoice_lines
  ADD COLUMN IF NOT EXISTS serial_number VARCHAR(80) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS serial_audit_id INTEGER,
  ADD COLUMN IF NOT EXISTS cost_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS cost_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS sell_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS sell_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS gross_profit DECIMAL(19,4) NOT NULL DEFAULT 0;

ALTER TABLE payments
  ADD COLUMN IF NOT EXISTS is_reversal BOOLEAN NOT NULL DEFAULT false,
  ADD COLUMN IF NOT EXISTS original_payment_id INTEGER,
  ADD COLUMN IF NOT EXISTS terminal_id VARCHAR(300),
  ADD COLUMN IF NOT EXISTS cash_drawer VARCHAR(30) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS current_windows_username VARCHAR(160) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS discount_given_on_payment DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS change_given DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS nett_amount_credited DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS amount_debited_to_account DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS refund_cash_amount DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS refund_as_credit_note_credited DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS refund_as_eftpos_dr DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS refund_as_eftpos_cr DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS credit_note_payment_credited DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS credit_note_amount_debited DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS refund_other_detail_amount DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS refund_other_detail_key VARCHAR(64) NOT NULL DEFAULT '';

-- payments.invoice_id had no foreign key at all and defaulted to a -1 sentinel for
-- "no invoice" - found via an orphaned row after deleting an invoice whose payment
-- was never cleaned up. No code path currently relies on the -1 sentinel, so this
-- switches to NULL (properly unconstrained) plus a real, cascading FK.
ALTER TABLE payments ALTER COLUMN invoice_id DROP NOT NULL;
ALTER TABLE payments ALTER COLUMN invoice_id DROP DEFAULT;
ALTER TABLE payments
  ADD CONSTRAINT payments_invoice_id_fkey FOREIGN KEY (invoice_id) REFERENCES invoice(invoice_id) ON DELETE CASCADE;

-- ==================== New tables: purchasing / goods received ====================

CREATE TABLE IF NOT EXISTS purchase_order (
  order_id INTEGER PRIMARY KEY,
  revision INTEGER NOT NULL DEFAULT -1,
  order_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  due_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  staff_id INTEGER REFERENCES staff(staff_id),
  supplier_id INTEGER REFERENCES supplier(supplier_id),
  order_no_suffix VARCHAR(30) NOT NULL DEFAULT '',
  delivery_address TEXT NOT NULL DEFAULT '',
  is_receiving BOOLEAN NOT NULL DEFAULT false,
  is_completed BOOLEAN NOT NULL DEFAULT false,
  is_closed_for_backorders BOOLEAN NOT NULL DEFAULT false,
  is_cancelled BOOLEAN NOT NULL DEFAULT false,
  comments TEXT NOT NULL DEFAULT '',
  date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS purchase_order_line (
  line_id INTEGER PRIMARY KEY,
  order_id INTEGER REFERENCES purchase_order(order_id),
  supplier_id INTEGER REFERENCES supplier(supplier_id),
  stock_id INTEGER REFERENCES stock(stock_id),
  supplier_code VARCHAR(80) NOT NULL DEFAULT '',
  goods_tax_code VARCHAR(14) NOT NULL DEFAULT '',
  cost_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  cost_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  quantity INTEGER NOT NULL DEFAULT 0,
  qty_received INTEGER NOT NULL DEFAULT 0,
  status VARCHAR(510) NOT NULL DEFAULT '',
  goods_id INTEGER,
  date_updated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS goods_received (
  goods_id INTEGER PRIMARY KEY,
  goods_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  staff_id INTEGER REFERENCES staff(staff_id),
  supplier_id INTEGER REFERENCES supplier(supplier_id),
  invoice_no VARCHAR(40) NOT NULL DEFAULT '',
  invoice_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  order_no_suffix VARCHAR(30) NOT NULL DEFAULT '',
  order_id INTEGER REFERENCES purchase_order(order_id),
  subtotal_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  subtotal_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  subtotal_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  freight_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  freight_tax_code VARCHAR(14) NOT NULL DEFAULT '',
  freight_tax_percentage DECIMAL(18,0) NOT NULL DEFAULT 0,
  freight_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  freight_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  discount_nett DECIMAL(19,4) NOT NULL DEFAULT 0,
  discount_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_expected DECIMAL(19,4) NOT NULL DEFAULT 0,
  comments TEXT NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS goods_received_line (
  line_id INTEGER PRIMARY KEY,
  goods_id INTEGER REFERENCES goods_received(goods_id),
  stock_id INTEGER REFERENCES stock(stock_id),
  goods_tax_code VARCHAR(14) NOT NULL DEFAULT '',
  goods_tax_percentage DECIMAL(18,0) NOT NULL DEFAULT 0,
  cost_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  cost_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  cost_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  sell_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  quantity INTEGER NOT NULL DEFAULT 0,
  total_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_inc DECIMAL(19,4) NOT NULL DEFAULT 0
);

ALTER TABLE purchase_order_line ADD CONSTRAINT fk_pol_goods FOREIGN KEY (goods_id) REFERENCES goods_received(goods_id);

-- ==================== New tables: sales orders ====================

CREATE TABLE IF NOT EXISTS sales_order (
  salesorder_id INTEGER PRIMARY KEY,
  salesorder_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  staff_id INTEGER REFERENCES staff(staff_id),
  customer_id INTEGER REFERENCES customer(customer_id),
  transaction_type VARCHAR(30) NOT NULL DEFAULT '',
  subtotal_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  subtotal_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  discount_nett DECIMAL(19,4) NOT NULL DEFAULT 0,
  discount_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  rounding DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  delivery_instructions TEXT NOT NULL DEFAULT '',
  comments TEXT NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS sales_order_line (
  line_id INTEGER PRIMARY KEY,
  salesorder_id INTEGER REFERENCES sales_order(salesorder_id),
  stock_id INTEGER REFERENCES stock(stock_id),
  description VARCHAR(80) NOT NULL DEFAULT '',
  cost_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  cost_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  sell_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  sales_tax_code VARCHAR(14) NOT NULL DEFAULT '',
  sales_tax_percentage DECIMAL(18,0) NOT NULL DEFAULT 0,
  sell_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  sell_actual_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  sell_actual_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  sell_actual_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  quantity DECIMAL(18,4) NOT NULL DEFAULT 0,
  total_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_inc DECIMAL(19,4) NOT NULL DEFAULT 0
);

-- ==================== New tables: cashup ====================

CREATE TABLE IF NOT EXISTS cashup_sessions (
  session_id INTEGER PRIMARY KEY,
  staff_id INTEGER REFERENCES staff(staff_id),
  staff_name VARCHAR(100) NOT NULL DEFAULT '',
  session_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  cash_drawer VARCHAR(30) NOT NULL DEFAULT '',
  current_windows_username VARCHAR(160) NOT NULL DEFAULT '',
  terminal_id VARCHAR(300) NOT NULL DEFAULT '',
  first_payment_id INTEGER NOT NULL DEFAULT -1,
  last_payment_id INTEGER NOT NULL DEFAULT -1,
  status VARCHAR(30) NOT NULL DEFAULT '',
  stock_value DECIMAL(19,4) NOT NULL DEFAULT 0,
  stock_variance DECIMAL(19,4) NOT NULL DEFAULT 0,
  comments TEXT NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS cashup_shortages (
  shortage_id INTEGER PRIMARY KEY,
  session_id INTEGER REFERENCES cashup_sessions(session_id),
  paymenttype_key VARCHAR(30) NOT NULL DEFAULT '',
  paymenttype_descr VARCHAR(62) NOT NULL DEFAULT '',
  amount_reported DECIMAL(19,4) NOT NULL DEFAULT 0,
  amount_counted DECIMAL(19,4) NOT NULL DEFAULT 0
);

-- ==================== New tables: layby ====================

CREATE TABLE IF NOT EXISTS layby (
  layby_id INTEGER PRIMARY KEY,
  layby_date_started TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  staff_id INTEGER REFERENCES staff(staff_id),
  customer_id INTEGER REFERENCES customer(customer_id),
  transaction_type VARCHAR(30) NOT NULL DEFAULT '',
  job_number INTEGER NOT NULL DEFAULT -1,
  terminal_id VARCHAR(300),
  cash_drawer VARCHAR(30) NOT NULL DEFAULT '',
  current_windows_username VARCHAR(160) NOT NULL DEFAULT '',
  subtotal_ex_non_taxable DECIMAL(19,4) NOT NULL DEFAULT 0,
  subtotal_ex_taxable DECIMAL(19,4) NOT NULL DEFAULT 0,
  subtotal_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  subtotal_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  discount_nett DECIMAL(19,4) NOT NULL DEFAULT 0,
  discount_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  rounding DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  is_cancelled BOOLEAN NOT NULL DEFAULT false,
  date_cancelled TIMESTAMP,
  cancelled_staff_id INTEGER NOT NULL DEFAULT -1,
  is_delivered BOOLEAN NOT NULL DEFAULT false,
  layby_date_delivered TIMESTAMP,
  layby_delivered_invoice_id INTEGER,
  delivery_instructions TEXT NOT NULL DEFAULT '',
  comments TEXT NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS layby_line (
  line_id INTEGER PRIMARY KEY,
  layby_id INTEGER REFERENCES layby(layby_id),
  stock_id INTEGER REFERENCES stock(stock_id),
  description VARCHAR(80) NOT NULL DEFAULT '',
  serial_number VARCHAR(80) NOT NULL DEFAULT '',
  serial_audit_id INTEGER,
  cost_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  cost_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  sell_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  sales_tax_code VARCHAR(14) NOT NULL DEFAULT '',
  sales_tax_percentage DECIMAL(18,0) NOT NULL DEFAULT 0,
  sell_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  sell_actual_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  sell_actual_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  sell_actual_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  quantity DECIMAL(18,4) NOT NULL DEFAULT 0,
  total_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_tax DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  gross_profit DECIMAL(19,4) NOT NULL DEFAULT 0
);

-- ==================== New tables: subscriptions (recurring billing) ====================

CREATE TABLE IF NOT EXISTS subscription (
  subscription_id INTEGER PRIMARY KEY,
  customer_id INTEGER REFERENCES customer(customer_id),
  staff_id INTEGER REFERENCES staff(staff_id),
  is_activated BOOLEAN NOT NULL DEFAULT false,
  start_date TIMESTAMP,
  termination_date TIMESTAMP,
  billing_period VARCHAR(30) NOT NULL DEFAULT '1M Monthly',
  terminal_id VARCHAR(300),
  is_cancelled BOOLEAN NOT NULL DEFAULT false,
  date_cancelled TIMESTAMP,
  cancelled_staff_id INTEGER NOT NULL DEFAULT -1,
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_updated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  comments TEXT NOT NULL DEFAULT '',
  ok_to_email_invoices BOOLEAN NOT NULL DEFAULT false
);

CREATE TABLE IF NOT EXISTS subscription_line (
  line_id INTEGER PRIMARY KEY,
  subscription_id INTEGER REFERENCES subscription(subscription_id),
  stock_id INTEGER REFERENCES stock(stock_id),
  stock_barcode VARCHAR(80) NOT NULL DEFAULT '',
  stock_description VARCHAR(80) NOT NULL DEFAULT '',
  sell_actual_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  quantity DECIMAL(18,4) NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS subscription_invoice (
  subs_invoice_line_id INTEGER PRIMARY KEY,
  subscription_id INTEGER REFERENCES subscription(subscription_id),
  invoice_id INTEGER REFERENCES invoice(invoice_id),
  invoice_period_start_date TIMESTAMP,
  invoice_period_end_date TIMESTAMP,
  email_sent_ok BOOLEAN NOT NULL DEFAULT false
);

-- ==================== New tables: stocktake ====================

CREATE TABLE IF NOT EXISTS stocktake (
  stocktake_id INTEGER PRIMARY KEY,
  stocktake_type VARCHAR(30) NOT NULL DEFAULT '',
  cat1 VARCHAR(30) NOT NULL DEFAULT '',
  cat2_list VARCHAR(4000) NOT NULL DEFAULT '',
  current_windows_username VARCHAR(160) NOT NULL DEFAULT '',
  terminal_id VARCHAR(300) NOT NULL DEFAULT '',
  is_committed BOOLEAN NOT NULL DEFAULT false,
  is_cancelled BOOLEAN NOT NULL DEFAULT false,
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  created_staff_name VARCHAR(100) NOT NULL DEFAULT '',
  date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  modified_staff_name VARCHAR(100) NOT NULL DEFAULT '',
  date_committed TIMESTAMP,
  committed_staff_name VARCHAR(100) NOT NULL DEFAULT '',
  comments TEXT NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS stocktake_items (
  item_id INTEGER PRIMARY KEY,
  stocktake_id INTEGER REFERENCES stocktake(stocktake_id),
  stock_id INTEGER REFERENCES stock(stock_id),
  barcode VARCHAR(80) NOT NULL DEFAULT '',
  cat1 VARCHAR(30) NOT NULL DEFAULT '',
  cat2 VARCHAR(30) NOT NULL DEFAULT '',
  description VARCHAR(80) NOT NULL DEFAULT '',
  qty_on_record INTEGER NOT NULL DEFAULT 0,
  qty_counted INTEGER NOT NULL DEFAULT 0,
  qty_difference INTEGER NOT NULL DEFAULT 0,
  date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS stocktake_serials (
  serial_number VARCHAR(80) NOT NULL DEFAULT '',
  stock_id INTEGER REFERENCES stock(stock_id)
);

-- ==================== New tables: supplier returns, serial audit, misc lookups ====================

CREATE TABLE IF NOT EXISTS supplier_returns (
  return_id INTEGER PRIMARY KEY,
  return_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  staff_id INTEGER REFERENCES staff(staff_id),
  staff_name VARCHAR(100) NOT NULL DEFAULT '',
  supplier_id INTEGER REFERENCES supplier(supplier_id),
  freight_tax VARCHAR(6) NOT NULL DEFAULT '',
  freight_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  freight_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  total_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  comments TEXT NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS supplier_return_line (
  line_id INTEGER PRIMARY KEY,
  return_id INTEGER REFERENCES supplier_returns(return_id),
  stock_id INTEGER REFERENCES stock(stock_id),
  serial_audit_id INTEGER,
  serial_number VARCHAR(80) NOT NULL DEFAULT '',
  invoice_no VARCHAR(40) NOT NULL DEFAULT '',
  ra_id INTEGER,
  supplier_rma_no VARCHAR(120) NOT NULL DEFAULT '',
  barcode VARCHAR(80) NOT NULL DEFAULT '',
  description VARCHAR(80) NOT NULL DEFAULT '',
  quantity INTEGER NOT NULL DEFAULT 0,
  symptoms VARCHAR(1022) NOT NULL DEFAULT '',
  request_notes VARCHAR(4080) NOT NULL DEFAULT '',
  goods_tax_code VARCHAR(6) NOT NULL DEFAULT '',
  cost_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  cost_inc DECIMAL(19,4) NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS serial_audit (
  serial_id INTEGER PRIMARY KEY,
  stock_id INTEGER REFERENCES stock(stock_id),
  serial_number VARCHAR(80) NOT NULL DEFAULT '',
  is_in_stock BOOLEAN NOT NULL DEFAULT false,
  status VARCHAR(30) NOT NULL DEFAULT '',
  warranty_date TIMESTAMP,
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS serial_audit_trail (
  trail_id INTEGER PRIMARY KEY,
  stock_id INTEGER REFERENCES stock(stock_id),
  serial_audit_id INTEGER REFERENCES serial_audit(serial_id),
  original_id INTEGER NOT NULL DEFAULT -1,
  tran_type VARCHAR(30) NOT NULL DEFAULT '',
  type_id INTEGER NOT NULL DEFAULT -1,
  type_line_id INTEGER NOT NULL DEFAULT -1,
  trail_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  movement INTEGER NOT NULL DEFAULT 0,
  is_rm_transaction BOOLEAN NOT NULL DEFAULT false,
  rm_tr_detail VARCHAR(510) NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS supplier_code (
  supcode VARCHAR(80) NOT NULL,
  supplier_id INTEGER REFERENCES supplier(supplier_id),
  stock_id INTEGER REFERENCES stock(stock_id),
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS stock_brands (
  brand_id INTEGER PRIMARY KEY,
  brand_name VARCHAR(50) NOT NULL DEFAULT '',
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS category1 (
  cat1_key VARCHAR(12) PRIMARY KEY,
  description VARCHAR(72) NOT NULL DEFAULT '',
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS category2 (
  cat2_key VARCHAR(12) PRIMARY KEY,
  description VARCHAR(72) NOT NULL DEFAULT '',
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS payment_details (
  detail_id INTEGER PRIMARY KEY,
  payment_id INTEGER REFERENCES payments(payment_id),
  payment_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  paymenttype_key VARCHAR(30) NOT NULL DEFAULT '',
  paymenttype_subkey VARCHAR(30) NOT NULL DEFAULT '',
  paymenttype_descr VARCHAR(126) NOT NULL DEFAULT '',
  amount DECIMAL(19,4) NOT NULL DEFAULT 0,
  comments VARCHAR(500) NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS payment_disbursements (
  disbursements_id INTEGER PRIMARY KEY,
  payment_id INTEGER REFERENCES payments(payment_id),
  invoice_id INTEGER REFERENCES invoice(invoice_id),
  tran_code VARCHAR(30) NOT NULL DEFAULT 'payment',
  source_of_funds VARCHAR(100) NOT NULL DEFAULT '',
  amount DECIMAL(19,4) NOT NULL DEFAULT 0
);

CREATE INDEX IF NOT EXISTS idx_gr_line_goods ON goods_received_line(goods_id);
CREATE INDEX IF NOT EXISTS idx_pol_order ON purchase_order_line(order_id);
CREATE INDEX IF NOT EXISTS idx_sol_order ON sales_order_line(salesorder_id);
CREATE INDEX IF NOT EXISTS idx_layby_line_layby ON layby_line(layby_id);
CREATE INDEX IF NOT EXISTS idx_sub_line_sub ON subscription_line(subscription_id);
CREATE INDEX IF NOT EXISTS idx_subs_inv_sub ON subscription_invoice(subscription_id);
CREATE INDEX IF NOT EXISTS idx_stocktake_items_st ON stocktake_items(stocktake_id);
CREATE INDEX IF NOT EXISTS idx_srl_return ON supplier_return_line(return_id);
CREATE INDEX IF NOT EXISTS idx_serial_audit_stock ON serial_audit(stock_id);
CREATE INDEX IF NOT EXISTS idx_sat_stock ON serial_audit_trail(stock_id);
CREATE INDEX IF NOT EXISTS idx_paydet_payment ON payment_details(payment_id);
CREATE INDEX IF NOT EXISTS idx_paydisb_payment ON payment_disbursements(payment_id);

COMMIT;

-- Widened post-hoc: legacy computed discount amounts (sell_ex - sellActual_ex) can exceed
-- NUMERIC(5,2) for bulk-quantity lines; this column holds a currency amount, not a percentage.
ALTER TABLE invoice_lines ALTER COLUMN discount TYPE NUMERIC(19,4);
