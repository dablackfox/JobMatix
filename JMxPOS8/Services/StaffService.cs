using System;
using System.Data;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services
{
    public class StaffService
    {
        private readonly DatabaseService _db;

        public StaffService(DatabaseService db)
        {
            _db = db;
        }

        public async Task<Staff?> FindStaffByBarcodeAsync(string barcode)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT staff_id, barcode, firstname, lastname, docket_name, 
                               position, isadministrator, inactive
                        FROM staff 
                        WHERE barcode = @barcode AND inactive = false
                        LIMIT 1";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@barcode";
                    param.Value = barcode;
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        if (await Task.Run(() => reader.Read()))
                        {
                            return new Staff
                            {
                                StaffId = Convert.ToInt32(reader["staff_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                FirstName = reader["firstname"].ToString() ?? "",
                                LastName = reader["lastname"].ToString() ?? "",
                                DocketName = reader["docket_name"].ToString() ?? "",
                                Position = reader["position"].ToString() ?? "",
                                IsAdministrator = Convert.ToBoolean(reader["isadministrator"]),
                                Inactive = Convert.ToBoolean(reader["inactive"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<Staff?> GetStaffByIdAsync(int staffId)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT staff_id, barcode, firstname, lastname, docket_name, 
                               position, isadministrator, inactive
                        FROM staff 
                        WHERE staff_id = @staffId
                        LIMIT 1";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@staffId";
                    param.Value = staffId;
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        if (await Task.Run(() => reader.Read()))
                        {
                            return new Staff
                            {
                                StaffId = Convert.ToInt32(reader["staff_id"]),
                                Barcode = reader["barcode"].ToString() ?? "",
                                FirstName = reader["firstname"].ToString() ?? "",
                                LastName = reader["lastname"].ToString() ?? "",
                                DocketName = reader["docket_name"].ToString() ?? "",
                                Position = reader["position"].ToString() ?? "",
                                IsAdministrator = Convert.ToBoolean(reader["isadministrator"]),
                                Inactive = Convert.ToBoolean(reader["inactive"])
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
