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
                    var post = new BaseCar()
                    {
                        LicensePlate = reader.GetString(0),
                        CarType = reader.GetString(1),
                        EngineCapacity = reader.GetInt32(2),
                        Fourdb = reader.GetBoolean(3),
                        EmployeeFirstName = reader.GetString(4),
                        EmployeeLastName = reader.GetString(5)
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



        private async Task<List<Car>> ReadFullCarsAsync(DbDataReader reader)
        {
            var posts = new List<Car>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Car(Db)
                    {
                        Id = reader.GetInt32(0),
                        LicensePlate = reader.GetString(1),
                        Fourdb = reader.GetBoolean(2),
                        EngineCapacity = reader.GetInt32(3),
                        ManufactureYear = reader.GetInt32(4),
                        Notes = reader.GetString(5),
                        CarCareDate = reader.GetDateTime(6),
                        EditDate = reader.GetDateTime(7),
                        carEmployeeId = reader.GetInt32(8),
                        EmployeeFirstName = reader.GetString(9),
                        EmployeeLastName = reader.GetString(10),
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
