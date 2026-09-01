using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services
{
    public class CustomerService
    {
        private readonly DatabaseService _db;

        public CustomerService(DatabaseService db)
        {
            _db = db;
        }

        public async Task<List<CustomerInvoiceSummary>> GetCustomerInvoicesAsync(int customerId, int limit = 100)
        {
            var items = new List<CustomerInvoiceSummary>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT invoice_id, invoicenumber, invoicedate, transactiontype, total_inc
                        FROM invoice
                        WHERE customer_id = @customerId AND transactiontype <> 'QUOTE'
                        ORDER BY invoicedate DESC
                        LIMIT {limit}";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@customerId";
                    param.Value = customerId;
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new CustomerInvoiceSummary
                            {
                                InvoiceId = Convert.ToInt32(reader["invoice_id"]),
                                InvoiceNumber = reader["invoicenumber"].ToString() ?? "",
                                InvoiceDate = Convert.ToDateTime(reader["invoicedate"]),
                                TransactionType = reader["transactiontype"].ToString() ?? "",
                                TotalInc = Convert.ToDecimal(reader["total_inc"])
                            });
                        }
                    }
                }
            }

            return items;
        }

        public async Task<List<CustomerItemSaleSummary>> GetCustomerItemSalesAsync(int customerId, int limit = 200)
        {
            var items = new List<CustomerItemSaleSummary>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT il.invoice_id, inv.invoicedate, il.description, il.quantity, il.unitprice, il.linetotal
                        FROM invoice_lines il
                        JOIN invoice inv ON inv.invoice_id = il.invoice_id
                        WHERE inv.customer_id = @customerId
                        ORDER BY inv.invoicedate DESC
                        LIMIT {limit}";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@customerId";
                    param.Value = customerId;
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new CustomerItemSaleSummary
                            {
                                InvoiceId = Convert.ToInt32(reader["invoice_id"]),
                                InvoiceDate = Convert.ToDateTime(reader["invoicedate"]),
                                Description = reader["description"].ToString() ?? "",
                                Quantity = Convert.ToDecimal(reader["quantity"]),
                                UnitPrice = Convert.ToDecimal(reader["unitprice"]),
                                LineTotal = Convert.ToDecimal(reader["linetotal"])
                            });
                        }
                    }
                }
            }

            return items;
        }

        public async Task<List<CustomerPaymentSummary>> GetCustomerPaymentsAsync(int customerId, int limit = 100)
        {
            var items = new List<CustomerPaymentSummary>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT invoice_id, paymentdate, paymentmethod, amount, transactiontype
                        FROM payments
                        WHERE customer_id = @customerId
                        ORDER BY paymentdate DESC
                        LIMIT {limit}";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@customerId";
                    param.Value = customerId;
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new CustomerPaymentSummary
                            {
                                InvoiceId = reader["invoice_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["invoice_id"]),
                                PaymentDate = Convert.ToDateTime(reader["paymentdate"]),
                                PaymentMethod = reader["paymentmethod"].ToString() ?? "",
                                Amount = Convert.ToDecimal(reader["amount"]),
                                TransactionType = reader["transactiontype"].ToString() ?? ""
                            });
                        }
                    }
                }
            }

            return items;
        }

        // Only possible as a direct query now that jobs and customers share one database
        // (see ROADMAP.md "What Changed" #13) - rmcustomer_id has a real FK to customer.
        public async Task<List<CustomerJobSummary>> GetCustomerJobsAsync(int customerId, int limit = 100)
        {
            var items = new List<CustomerJobSummary>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT job_id, dateupdated, techstaffname, jobstatus, goodsincare, problemshort, problemlong, problemsymptoms, priority
                        FROM jobs
                        WHERE rmcustomer_id = @customerId
                        ORDER BY job_id DESC
                        LIMIT {limit}";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@customerId";
                    param.Value = customerId;
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new CustomerJobSummary
                            {
                                JobId = Convert.ToInt32(reader["job_id"]),
                                DateUpdated = Convert.ToDateTime(reader["dateupdated"]),
                                TechStaffName = reader["techstaffname"].ToString() ?? "",
                                JobStatus = reader["jobstatus"].ToString() ?? "",
                                GoodsInCare = reader["goodsincare"].ToString() ?? "",
                                ProblemShort = reader["problemshort"].ToString() ?? "",
                                ProblemLong = reader["problemlong"].ToString() ?? "",
                                ProblemSymptoms = reader["problemsymptoms"].ToString() ?? "",
                                Priority = reader["priority"].ToString() ?? ""
                            });
                        }
                    }
                }
            }

            return items;
        }

        public async Task<List<Customer>> GetAllCustomersAsync(int limit = 100)
        {
            var customers = new List<Customer>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT customer_id, barcode, customername, companyname, grade,
                               address, suburb, state, postcode,
                               homephone, businessphone, mobile, emailaddress,
                               isaccount, accountbalance, creditlimit, inactive
                        FROM customer 
                        WHERE inactive = false
                        ORDER BY customername 
                        LIMIT {limit}";

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            customers.Add(new Customer
                            {
                                CustomerId = Convert.ToInt32(reader["customer_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                CustomerName = reader["customername"].ToString() ?? "",
                                CompanyName = reader["companyname"].ToString() ?? "",
                                Grade = reader["grade"].ToString() ?? "",
                                Address = reader["address"].ToString() ?? "",
                                Suburb = reader["suburb"].ToString() ?? "",
                                State = reader["state"].ToString() ?? "",
                                Postcode = reader["postcode"].ToString() ?? "",
                                HomePhone = reader["homephone"].ToString() ?? "",
                                BusinessPhone = reader["businessphone"].ToString() ?? "",
                                Mobile = reader["mobile"].ToString() ?? "",
                                EmailAddress = reader["emailaddress"].ToString() ?? "",
                                IsAccount = Convert.ToBoolean(reader["isaccount"]),
                                AccountBalance = Convert.ToDecimal(reader["accountbalance"]),
                                CreditLimit = Convert.ToDecimal(reader["creditlimit"]),
                                Inactive = Convert.ToBoolean(reader["inactive"])
                            });
                        }
                    }
                }
            }

            return customers;
        }

        public async Task<Customer?> FindCustomerByBarcodeAsync(string barcode)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT customer_id, barcode, customername, companyname, grade,
                               address, suburb, state, postcode,
                               homephone, businessphone, mobile, emailaddress,
                               isaccount, accountbalance, creditlimit, inactive
                        FROM customer 
                        WHERE barcode = @barcode
                        LIMIT 1";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@barcode";
                    param.Value = barcode;
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        if (await Task.Run(() => reader.Read()))
                        {
                            return new Customer
                            {
                                CustomerId = Convert.ToInt32(reader["customer_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                CustomerName = reader["customername"].ToString() ?? "",
                                CompanyName = reader["companyname"].ToString() ?? "",
                                Grade = reader["grade"].ToString() ?? "",
                                Address = reader["address"].ToString() ?? "",
                                Suburb = reader["suburb"].ToString() ?? "",
                                State = reader["state"].ToString() ?? "",
                                Postcode = reader["postcode"].ToString() ?? "",
                                HomePhone = reader["homephone"].ToString() ?? "",
                                BusinessPhone = reader["businessphone"].ToString() ?? "",
                                Mobile = reader["mobile"].ToString() ?? "",
                                EmailAddress = reader["emailaddress"].ToString() ?? "",
                                IsAccount = Convert.ToBoolean(reader["isaccount"]),
                                AccountBalance = Convert.ToDecimal(reader["accountbalance"]),
                                CreditLimit = Convert.ToDecimal(reader["creditlimit"]),
                                Inactive = Convert.ToBoolean(reader["inactive"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT customer_id, barcode, customername, companyname, grade,
                               address, suburb, state, postcode,
                               homephone, businessphone, mobile, emailaddress,
                               isaccount, accountbalance, creditlimit, inactive
                        FROM customer
                        WHERE customer_id = @customerId
                        LIMIT 1";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@customerId";
                    param.Value = customerId;
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        if (await Task.Run(() => reader.Read()))
                        {
                            return new Customer
                            {
                                CustomerId = Convert.ToInt32(reader["customer_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                CustomerName = reader["customername"].ToString() ?? "",
                                CompanyName = reader["companyname"].ToString() ?? "",
                                Grade = reader["grade"].ToString() ?? "",
                                Address = reader["address"].ToString() ?? "",
                                Suburb = reader["suburb"].ToString() ?? "",
                                State = reader["state"].ToString() ?? "",
                                Postcode = reader["postcode"].ToString() ?? "",
                                HomePhone = reader["homephone"].ToString() ?? "",
                                BusinessPhone = reader["businessphone"].ToString() ?? "",
                                Mobile = reader["mobile"].ToString() ?? "",
                                EmailAddress = reader["emailaddress"].ToString() ?? "",
                                IsAccount = Convert.ToBoolean(reader["isaccount"]),
                                AccountBalance = Convert.ToDecimal(reader["accountbalance"]),
                                CreditLimit = Convert.ToDecimal(reader["creditlimit"]),
                                Inactive = Convert.ToBoolean(reader["inactive"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<Customer>> SearchCustomersAsync(string searchTerm, int limit = 50)
        {
            var customers = new List<Customer>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT customer_id, barcode, customername, companyname, grade,
                               address, suburb, state, postcode,
                               homephone, businessphone, mobile, emailaddress,
                               isaccount, accountbalance, creditlimit, inactive
                        FROM customer 
                        WHERE inactive = false
                          AND (LOWER(customername) LIKE LOWER(@search) 
                           OR LOWER(companyname) LIKE LOWER(@search)
                           OR LOWER(barcode) LIKE LOWER(@search))
                        ORDER BY customername 
                        LIMIT {limit}";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@search";
                    param.Value = $"%{searchTerm}%";
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            customers.Add(new Customer
                            {
                                CustomerId = Convert.ToInt32(reader["customer_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                CustomerName = reader["customername"].ToString() ?? "",
                                CompanyName = reader["companyname"].ToString() ?? "",
                                Grade = reader["grade"].ToString() ?? "",
                                Address = reader["address"].ToString() ?? "",
                                Suburb = reader["suburb"].ToString() ?? "",
                                State = reader["state"].ToString() ?? "",
                                Postcode = reader["postcode"].ToString() ?? "",
                                HomePhone = reader["homephone"].ToString() ?? "",
                                BusinessPhone = reader["businessphone"].ToString() ?? "",
                                Mobile = reader["mobile"].ToString() ?? "",
                                EmailAddress = reader["emailaddress"].ToString() ?? "",
                                IsAccount = Convert.ToBoolean(reader["isaccount"]),
                                AccountBalance = Convert.ToDecimal(reader["accountbalance"]),
                                CreditLimit = Convert.ToDecimal(reader["creditlimit"]),
                                Inactive = Convert.ToBoolean(reader["inactive"])
                            });
                        }
                    }
                }
            }

            return customers;
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO customer (
                            barcode, customername, companyname, grade, inactive,
                            contactname, contactposition, address, suburb, state, postcode, country,
                            businessphone, homephone, fax, mobile, emailaddress, website,
                            abn, taxcode, isaccount, accountbalance, creditlimit, notes
                        ) VALUES (
                            @barcode, @customername, @companyname, @grade, @inactive,
                            @contactname, @contactposition, @address, @suburb, @state, @postcode, @country,
                            @businessphone, @homephone, @fax, @mobile, @emailaddress, @website,
                            @abn, @taxcode, @isaccount, @accountbalance, @creditlimit, @notes
                        )
                        RETURNING customer_id";

                    AddCustomerParameters(cmd, customer);

                    var result = await Task.Run(() => cmd.ExecuteScalar());
                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE customer SET
                            barcode = @barcode,
                            customername = @customername,
                            companyname = @companyname,
                            grade = @grade,
                            inactive = @inactive,
                            contactname = @contactname,
                            contactposition = @contactposition,
                            address = @address,
                            suburb = @suburb,
                            state = @state,
                            postcode = @postcode,
                            country = @country,
                            businessphone = @businessphone,
                            homephone = @homephone,
                            fax = @fax,
                            mobile = @mobile,
                            emailaddress = @emailaddress,
                            website = @website,
                            abn = @abn,
                            taxcode = @taxcode,
                            isaccount = @isaccount,
                            accountbalance = @accountbalance,
                            creditlimit = @creditlimit,
                            notes = @notes
                        WHERE customer_id = @customer_id";

                    AddCustomerParameters(cmd, customer);
                    
                    var idParam = cmd.CreateParameter();
                    idParam.ParameterName = "@customer_id";
                    idParam.Value = customer.CustomerId;
                    cmd.Parameters.Add(idParam);

                    await Task.Run(() => cmd.ExecuteNonQuery());
                }
            }
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    // Soft delete by setting inactive flag
                    cmd.CommandText = @"
                        UPDATE customer 
                        SET inactive = true 
                        WHERE customer_id = @customer_id";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@customer_id";
                    param.Value = customerId;
                    cmd.Parameters.Add(param);

                    await Task.Run(() => cmd.ExecuteNonQuery());
                }
            }
        }

        private void AddCustomerParameters(IDbCommand cmd, Customer customer)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@barcode"] = customer.Barcode,
                ["@customername"] = customer.CustomerName,
                ["@companyname"] = customer.CompanyName,
                ["@grade"] = customer.Grade,
                ["@inactive"] = customer.Inactive,
                ["@contactname"] = customer.ContactName,
                ["@contactposition"] = customer.ContactPosition,
                ["@address"] = customer.Address,
                ["@suburb"] = customer.Suburb,
                ["@state"] = customer.State,
                ["@postcode"] = customer.Postcode,
                ["@country"] = customer.Country,
                ["@businessphone"] = customer.BusinessPhone,
                ["@homephone"] = customer.HomePhone,
                ["@fax"] = customer.Fax,
                ["@mobile"] = customer.Mobile,
                ["@emailaddress"] = customer.EmailAddress,
                ["@website"] = customer.Website,
                ["@abn"] = customer.Abn,
                ["@taxcode"] = customer.TaxCode,
                ["@isaccount"] = customer.IsAccount,
                ["@accountbalance"] = customer.AccountBalance,
                ["@creditlimit"] = customer.CreditLimit,
                ["@notes"] = customer.Notes
            };

            foreach (var kvp in parameters)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = kvp.Key;
                param.Value = kvp.Value ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }
        }
    }
}
