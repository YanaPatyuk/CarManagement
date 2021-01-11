using CarsMangment.Const;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace CarsMangment.Model
{
    public class DatabaseQuery
    {
        public Database Db { get; }

        public DatabaseQuery(Database db)
        {
            Db = db;
        }


        public async Task<List<Car_Type>> AllCarTypes()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = SqlQuerys.GetAllCarTypes;
            return await ReadAllAllCarTypesAsyn(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<Car_Type>> ReadAllAllCarTypesAsyn(DbDataReader reader)
        {
            var posts =  new List<Car_Type>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Car_Type()
                    {
                        ID = reader.GetInt32(0),
                        Type_name = reader.GetString(1)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public async Task<List<Employee>> AllEmployees()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = SqlQuerys.GetAllEmployees;
            return await ReadAllAllEmployeesAsyn(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<Employee>> ReadAllAllEmployeesAsyn(DbDataReader reader)
        {
            var posts = new List<Employee>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Employee()
                    {
                        ID = reader.GetInt32(0),
                        Firstname = reader.GetString(1),
                        Lastname = reader.GetString(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public async Task<List<BaseCar>> AllCarsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = SqlQuerys.GetAllCarsBaseInfo;
            return await ReadBaseCarsAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<BaseCar>> ReadBaseCarsAsync(DbDataReader reader)
        {
            var posts = new List<BaseCar>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    int colIndex = reader.GetOrdinal("first_name");
                    string first_name = reader.IsDBNull(colIndex) ?string.Empty : reader.GetString(4);
                    colIndex = reader.GetOrdinal("last_name");
                    string last_name = reader.IsDBNull(colIndex) ? string.Empty : reader.GetString(5);
                    colIndex = reader.GetOrdinal("engine_capacity");
                    int engine_capacity = reader.IsDBNull(colIndex) ? 0 : reader.GetInt32(2);
                    var post = new BaseCar()
                    {
                        LicensePlate = reader.GetString(0),
                        CarType = reader.GetString(1),
                        EngineCapacity = engine_capacity,
                        Fourdb = reader.GetBoolean(3),
                        EmployeeFirstName = first_name,
                        EmployeeLastName = last_name
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public async Task<Car> FindOneCarAsync(string license_plate)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = SqlQuerys.GetFullCarInfoByLicense;
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@license_plate",
                DbType = DbType.String,
                Value = license_plate,
            });
            var result = await ReadFullCarsAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<Car> FindOneCarAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = SqlQuerys.GetFullCarInfoById;
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadFullCarsAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<int> GetTypeID(string type_name)
        {
            using var cmd = Db.Connection.CreateCommand();
            //add car type to car type table
            cmd.CommandText = SqlQuerys.InsertNewCarType;
            BindTypeParam(cmd, type_name);
            await cmd.ExecuteNonQueryAsync();
            //get the car type id
            cmd.CommandText = SqlQuerys.GetTypeID;
            var result = await ReadCarTypeIdAsync(await cmd.ExecuteReaderAsync());
            return result;

        }


        private void BindTypeParam(MySqlCommand cmd, string name)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@type_name",
                DbType = DbType.String,
                Value = name,
            });
        }

        private async Task<int> ReadCarTypeIdAsync(DbDataReader reader)
        {
            int id = -1;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    id = reader.GetInt16(0);
                }
            }
            return id;
        }

        private async Task<List<Car>> ReadFullCarsAsync(DbDataReader reader)
        {
            var posts = new List<Car>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    int colIndex = reader.GetOrdinal("first_name");
                    string first_name = reader.IsDBNull(colIndex) ? string.Empty : reader.GetString(9);
                    colIndex = reader.GetOrdinal("last_name");
                    string last_name = reader.IsDBNull(colIndex) ? string.Empty : reader.GetString(10);
                    colIndex = reader.GetOrdinal("notes");
                    string notes = reader.IsDBNull(colIndex) ? string.Empty : reader.GetString(5);
                    colIndex = reader.GetOrdinal("employee");
                    int employee_id = reader.IsDBNull(colIndex) ? 0 : reader.GetInt32(8);
                    colIndex = reader.GetOrdinal("engine_capacity");
                    int engine_capacity = reader.IsDBNull(colIndex) ? 0: reader.GetInt32(3);

                    var post = new Car(Db)
                    {
                        Id = reader.GetInt32(0),
                        LicensePlate = reader.GetString(1),
                        Fourdb = reader.GetBoolean(2),
                        EngineCapacity = engine_capacity,
                        ManufactureYear = reader.GetInt32(4),
                        Notes = notes,
                        CarCareDate = reader.GetDateTime(6),
                        EditDate = reader.GetDateTime(7),
                        carEmployeeId = employee_id,
                        EmployeeFirstName = first_name,
                        EmployeeLastName = last_name,
                        carTypeId = reader.GetInt32(11),
                        CarType = reader.GetString(12),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

    }
}
