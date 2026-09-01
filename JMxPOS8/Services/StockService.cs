using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services
{
    public class StockService
    {
        private readonly DatabaseService _db;

        public StockService(DatabaseService db)
        {
            _db = db;
        }

        public async Task<List<StockItem>> GetAllStockAsync(int limit = 100)
        {
            var items = new List<StockItem>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT stock_id, barcode, stockcode, description, category, 
                               quantityinstock, costprice, sellprice, inactive
                        FROM stock 
                        WHERE inactive = false
                        ORDER BY stockcode 
                        LIMIT {limit}";

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new StockItem
                            {
                                StockId = Convert.ToInt32(reader["stock_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                StockCode = reader["stockcode"].ToString() ?? "",
                                Description = reader["description"].ToString() ?? "",
                                Category = reader["category"].ToString() ?? "",
                                QuantityInStock = Convert.ToDecimal(reader["quantityinstock"]),
                                CostPrice = Convert.ToDecimal(reader["costprice"]),
                                SellPrice = Convert.ToDecimal(reader["sellprice"]),
                                Inactive = Convert.ToBoolean(reader["inactive"])
                            });
                        }
                    }
                }
            }

            return items;
        }

        public async Task<StockItem?> FindStockByBarcodeAsync(string barcode)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT stock_id, barcode, stockcode, description, category, 
                               quantityinstock, costprice, sellprice, inactive, requiresserial
                        FROM stock 
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
                            return new StockItem
                            {
                                StockId = Convert.ToInt32(reader["stock_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                StockCode = reader["stockcode"].ToString() ?? "",
                                Description = reader["description"].ToString() ?? "",
                                Category = reader["category"].ToString() ?? "",
                                QuantityInStock = Convert.ToDecimal(reader["quantityinstock"]),
                                CostPrice = Convert.ToDecimal(reader["costprice"]),
                                SellPrice = Convert.ToDecimal(reader["sellprice"]),
                                Inactive = Convert.ToBoolean(reader["inactive"]),
                                RequiresSerial = Convert.ToBoolean(reader["requiresserial"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<StockItem?> GetStockByIdAsync(int stockId)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT stock_id, barcode, stockcode, description, category,
                               quantityinstock, costprice, sellprice, inactive, requiresserial
                        FROM stock
                        WHERE stock_id = @stockId
                        LIMIT 1";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@stockId";
                    param.Value = stockId;
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        if (await Task.Run(() => reader.Read()))
                        {
                            return new StockItem
                            {
                                StockId = Convert.ToInt32(reader["stock_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                StockCode = reader["stockcode"].ToString() ?? "",
                                Description = reader["description"].ToString() ?? "",
                                Category = reader["category"].ToString() ?? "",
                                QuantityInStock = Convert.ToDecimal(reader["quantityinstock"]),
                                CostPrice = Convert.ToDecimal(reader["costprice"]),
                                SellPrice = Convert.ToDecimal(reader["sellprice"]),
                                Inactive = Convert.ToBoolean(reader["inactive"]),
                                RequiresSerial = Convert.ToBoolean(reader["requiresserial"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<StockItem>> SearchStockAsync(string searchTerm, int limit = 50)
        {
            var items = new List<StockItem>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT stock_id, barcode, stockcode, description, category, 
                               quantityinstock, costprice, sellprice, inactive, requiresserial
                        FROM stock 
                        WHERE inactive = false
                          AND (LOWER(stockcode) LIKE LOWER(@search) 
                           OR LOWER(description) LIKE LOWER(@search)
                           OR LOWER(barcode) LIKE LOWER(@search))
                        ORDER BY stockcode 
                        LIMIT {limit}";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@search";
                    param.Value = $"%{searchTerm}%";
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new StockItem
                            {
                                StockId = Convert.ToInt32(reader["stock_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                StockCode = reader["stockcode"].ToString() ?? "",
                                Description = reader["description"].ToString() ?? "",
                                Category = reader["category"].ToString() ?? "",
                                QuantityInStock = Convert.ToDecimal(reader["quantityinstock"]),
                                CostPrice = Convert.ToDecimal(reader["costprice"]),
                                SellPrice = Convert.ToDecimal(reader["sellprice"]),
                                Inactive = Convert.ToBoolean(reader["inactive"]),
                                RequiresSerial = Convert.ToBoolean(reader["requiresserial"])
                            });
                        }
                    }
                }
            }

            return items;
        }

        public async Task<bool> UpdateStockQuantityAsync(int stockId, decimal newQuantity)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE stock 
                        SET quantityinstock = @quantity,
                            date_modified = CURRENT_TIMESTAMP
                        WHERE stock_id = @stockId";

                    var param1 = cmd.CreateParameter();
                    param1.ParameterName = "@quantity";
                    param1.Value = newQuantity;
                    cmd.Parameters.Add(param1);

                    var param2 = cmd.CreateParameter();
                    param2.ParameterName = "@stockId";
                    param2.Value = stockId;
                    cmd.Parameters.Add(param2);

                    int rows = await Task.Run(() => cmd.ExecuteNonQuery());
                    return rows > 0;
                }
            }
        }

        public async Task<int> AddStockAsync(StockItem stock)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO stock (
                            supplier_id, barcode, stockcode, description, category, quantityinstock,
                            costprice, sellprice, inactive, requiresserial,
                            minstocklevel, reorderquantity, notes
                        ) VALUES (
                            1, @barcode, @stockcode, @description, @category, @quantityinstock,
                            @costprice, @sellprice, @inactive, @requiresserial,
                            @reorderlevel, @reorderquantity, @notes
                        )
                        RETURNING stock_id";

                    AddStockParameters(cmd, stock);

                    Console.WriteLine($"[SQL INSERT] {cmd.CommandText}");
                    Console.WriteLine($"[PARAMS] barcode={stock.Barcode}, code={stock.StockCode}, desc={stock.Description}, requiresserial={stock.RequiresSerial}");

                    var result = await Task.Run(() => cmd.ExecuteScalar());
                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task UpdateStockAsync(StockItem stock)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE stock SET
                            barcode = @barcode,
                            stockcode = @stockcode,
                            description = @description,
                            category = @category,
                            quantityinstock = @quantityinstock,
                            costprice = @costprice,
                            sellprice = @sellprice,
                            inactive = @inactive,
                            requiresserial = @requiresserial,
                            minstocklevel = @minstocklevel,
                            reorderquantity = @reorderquantity,
                            notes = @notes
                        WHERE stock_id = @stock_id";

                    AddStockParameters(cmd, stock);
                    
                    var idParam = cmd.CreateParameter();
                    idParam.ParameterName = "@stock_id";
                    idParam.Value = stock.StockId;
                    cmd.Parameters.Add(idParam);

                    Console.WriteLine($"[SQL UPDATE] {cmd.CommandText}");
                    Console.WriteLine($"[PARAMS] stock_id={stock.StockId}, barcode={stock.Barcode}, requiresserial={stock.RequiresSerial}");

                    await Task.Run(() => cmd.ExecuteNonQuery());
                }
            }
        }

        public async Task DeleteStockAsync(int stockId)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    // Soft delete by setting inactive flag
                    cmd.CommandText = @"
                        UPDATE stock 
                        SET inactive = true 
                        WHERE stock_id = @stock_id";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@stock_id";
                    param.Value = stockId;
                    cmd.Parameters.Add(param);

                    await Task.Run(() => cmd.ExecuteNonQuery());
                }
            }
        }

        private void AddStockParameters(System.Data.IDbCommand cmd, StockItem stock)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@barcode"] = stock.Barcode,
                ["@stockcode"] = stock.StockCode,
                ["@description"] = stock.Description,
                ["@category"] = stock.Category,
                ["@quantityinstock"] = stock.QuantityInStock,
                ["@costprice"] = stock.CostPrice,
                ["@sellprice"] = stock.SellPrice,
                ["@inactive"] = stock.Inactive,
                ["@requiresserial"] = stock.RequiresSerial,
                ["@minstocklevel"] = stock.ReorderLevel,
                ["@reorderquantity"] = stock.ReorderQuantity,
                ["@supplier"] = stock.Supplier,
                ["@location"] = stock.Location,
                ["@notes"] = stock.Notes
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
