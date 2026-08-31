using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services
{
    public class StaffService
    {
        private readonly DatabaseService _db;

        private const string SelectColumns = @"
            staff_id, barcode, firstname, lastname, docket_name, position, isadministrator, inactive,
            dateofbirth, address, suburb, state, postcode, homephone, mobile, emailaddress, status,
            password, passwordhint";

        public StaffService(DatabaseService db)
        {
            _db = db;
        }

        public async Task<List<Staff>> GetAllStaffAsync(int limit = 100)
        {
            var items = new List<Staff>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    // staff_id <= 0 are legacy sentinel rows (<Deleted>/<Default>), not real people
                    cmd.CommandText = $@"
                        SELECT {SelectColumns}
                        FROM staff
                        WHERE inactive = false AND staff_id > 0
                        ORDER BY lastname, firstname
                        LIMIT {limit}";

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                            items.Add(MapStaff(reader));
                    }
                }
            }

            return items;
        }

        public async Task<Staff?> FindStaffByBarcodeAsync(string barcode)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT {SelectColumns}
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
                            return MapStaff(reader);
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
                    cmd.CommandText = $@"
                        SELECT {SelectColumns}
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
                            return MapStaff(reader);
                    }
                }
            }

            return null;
        }

        public async Task<List<Staff>> SearchStaffAsync(string searchTerm, int limit = 50)
        {
            var items = new List<Staff>();

            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        SELECT {SelectColumns}
                        FROM staff
                        WHERE inactive = false AND staff_id > 0
                          AND (LOWER(firstname) LIKE LOWER(@search)
                           OR LOWER(lastname) LIKE LOWER(@search)
                           OR LOWER(docket_name) LIKE LOWER(@search)
                           OR LOWER(barcode) LIKE LOWER(@search))
                        ORDER BY lastname, firstname
                        LIMIT {limit}";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@search";
                    param.Value = $"%{searchTerm}%";
                    cmd.Parameters.Add(param);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                            items.Add(MapStaff(reader));
                    }
                }
            }

            return items;
        }

        public async Task<int> AddStaffAsync(Staff staff)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO staff (
                            barcode, firstname, lastname, docket_name, position, isadministrator, inactive,
                            dateofbirth, address, suburb, state, postcode, homephone, mobile, emailaddress,
                            status, password, passwordhint
                        ) VALUES (
                            @barcode, @firstname, @lastname, @docketname, @position, @isadministrator, @inactive,
                            @dateofbirth, @address, @suburb, @state, @postcode, @homephone, @mobile, @emailaddress,
                            @status, @password, @passwordhint
                        )
                        RETURNING staff_id";

                    AddStaffParameters(cmd, staff);

                    var result = await Task.Run(() => cmd.ExecuteScalar());
                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task UpdateStaffAsync(Staff staff)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE staff SET
                            barcode = @barcode,
                            firstname = @firstname,
                            lastname = @lastname,
                            docket_name = @docketname,
                            position = @position,
                            isadministrator = @isadministrator,
                            inactive = @inactive,
                            dateofbirth = @dateofbirth,
                            address = @address,
                            suburb = @suburb,
                            state = @state,
                            postcode = @postcode,
                            homephone = @homephone,
                            mobile = @mobile,
                            emailaddress = @emailaddress,
                            status = @status,
                            password = @password,
                            passwordhint = @passwordhint
                        WHERE staff_id = @staff_id";

                    AddStaffParameters(cmd, staff);

                    var idParam = cmd.CreateParameter();
                    idParam.ParameterName = "@staff_id";
                    idParam.Value = staff.StaffId;
                    cmd.Parameters.Add(idParam);

                    await Task.Run(() => cmd.ExecuteNonQuery());
                }
            }
        }

        public async Task DeleteStaffAsync(int staffId)
        {
            using (var conn = _db.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    // Soft delete by setting inactive flag - staff rows are referenced by
                    // years of invoices/payments/etc, so they're never actually removed.
                    cmd.CommandText = @"
                        UPDATE staff
                        SET inactive = true
                        WHERE staff_id = @staff_id";

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@staff_id";
                    param.Value = staffId;
                    cmd.Parameters.Add(param);

                    await Task.Run(() => cmd.ExecuteNonQuery());
                }
            }
        }

        private static Staff MapStaff(IDataReader reader)
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
                Inactive = Convert.ToBoolean(reader["inactive"]),
                DateOfBirth = reader["dateofbirth"] is DBNull ? null : Convert.ToDateTime(reader["dateofbirth"]),
                Address = reader["address"].ToString() ?? "",
                Suburb = reader["suburb"].ToString() ?? "",
                State = reader["state"].ToString() ?? "",
                Postcode = reader["postcode"].ToString() ?? "",
                HomePhone = reader["homephone"].ToString() ?? "",
                Mobile = reader["mobile"].ToString() ?? "",
                EmailAddress = reader["emailaddress"].ToString() ?? "",
                Status = reader["status"].ToString() ?? "",
                Password = reader["password"].ToString() ?? "",
                PasswordHint = reader["passwordhint"].ToString() ?? ""
            };
        }

        private void AddStaffParameters(IDbCommand cmd, Staff staff)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@barcode"] = staff.Barcode,
                ["@firstname"] = staff.FirstName,
                ["@lastname"] = staff.LastName,
                ["@docketname"] = staff.DocketName,
                ["@position"] = staff.Position,
                ["@isadministrator"] = staff.IsAdministrator,
                ["@inactive"] = staff.Inactive,
                ["@dateofbirth"] = (object?)staff.DateOfBirth ?? DBNull.Value,
                ["@address"] = staff.Address,
                ["@suburb"] = staff.Suburb,
                ["@state"] = staff.State,
                ["@postcode"] = staff.Postcode,
                ["@homephone"] = staff.HomePhone,
                ["@mobile"] = staff.Mobile,
                ["@emailaddress"] = staff.EmailAddress,
                ["@status"] = staff.Status,
                ["@password"] = staff.Password,
                ["@passwordhint"] = staff.PasswordHint
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
