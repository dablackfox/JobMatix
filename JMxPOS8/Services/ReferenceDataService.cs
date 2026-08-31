using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services
{
    // Table/column names for one flat lookup table. Fixed, hardcoded configs only (see
    // ReferenceTables below) - never build one from user input, since the names are
    // interpolated directly into SQL (table/column identifiers can't be parameterized).
    public sealed record ReferenceTableConfig(string TableName, string IdColumn, string DescriptionColumn, int MaxLength);

    // GoodsTypes/Brands/Symptoms/TaskTypes all share the identical id/description shape,
    // matching the legacy app's single parameterized frmListEdit form used for all of them
    // (ROADMAP.md Phase 3 - "Brand/model reference data").
    public static class ReferenceTables
    {
        public static readonly ReferenceTableConfig GoodsTypes = new("goodstypes", "goodstype_id", "goodstypedescription", 50);
        public static readonly ReferenceTableConfig Brands = new("brands", "brand_id", "branddescr", 50);
        public static readonly ReferenceTableConfig Symptoms = new("symptoms", "symptom_id", "symptomdescr", 50);
        public static readonly ReferenceTableConfig TaskTypes = new("tasktypes", "tasktype_id", "taskdescription", 50);
    }

    public class ReferenceDataService
    {
        private readonly DatabaseService _db;

        public ReferenceDataService(DatabaseService db)
        {
            _db = db;
        }

        public async Task<List<ReferenceItem>> GetAllAsync(ReferenceTableConfig config)
        {
            var items = new List<ReferenceItem>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT {config.IdColumn}, {config.DescriptionColumn}
                        FROM {config.TableName}
                        ORDER BY {config.DescriptionColumn}";

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new ReferenceItem
                            {
                                Id = Convert.ToInt32(reader[0]),
                                Description = reader[1]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }

            return items;
        }

        public async Task<int> AddAsync(ReferenceTableConfig config, string description)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        INSERT INTO {config.TableName} ({config.DescriptionColumn})
                        VALUES (@descr)
                        RETURNING {config.IdColumn}";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@descr";
                    param.Value = description;
                    cmd.Parameters.Add(param);

                    var result = await Task.Run(() => cmd.ExecuteScalar());
                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task UpdateAsync(ReferenceTableConfig config, int id, string description)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        UPDATE {config.TableName}
                        SET {config.DescriptionColumn} = @descr
                        WHERE {config.IdColumn} = @id";

                    var descrParam = cmd.CreateParameter();
                    descrParam.ParameterName = "@descr";
                    descrParam.Value = description;
                    cmd.Parameters.Add(descrParam);

                    var idParam = cmd.CreateParameter();
                    idParam.ParameterName = "@id";
                    idParam.Value = id;
                    cmd.Parameters.Add(idParam);

                    await Task.Run(() => cmd.ExecuteNonQuery());
                }
            }
        }

        public async Task DeleteAsync(ReferenceTableConfig config, int id)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {config.TableName} WHERE {config.IdColumn} = @id";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@id";
                    param.Value = id;
                    cmd.Parameters.Add(param);

                    await Task.Run(() => cmd.ExecuteNonQuery());
                }
            }
        }
    }
}
