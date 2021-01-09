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
            cmd.CommandText = @"SELECT * FROM `carsdb`.`car_type`;";
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
            cmd.CommandText = @"SELECT * FROM `carsdb`.`employee`;";
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
            cmd.CommandText = @"SELECT  license_plate,car_type,fourdb,Engine_capacity,employee FROM `carsdb`.`car`;";
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
                        License_plate = reader.GetString(0),
                        Car_type = reader.GetInt32(1),
                        Fourdb = reader.GetBoolean(2),
                        Engine_capacity = reader.GetInt32(3),
                        Employee = reader.GetInt32(4)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public async Task<Car> FindOneCarAsync(string license_plate)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `carsdb`.`car` WHERE `license_plate` = @license_plate";
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
            cmd.CommandText = @"SELECT * FROM `carsdb`.`car` WHERE `id` = @id";
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
                        License_plate = reader.GetString(1),
                        Car_type = reader.GetInt32(2),
                        Fourdb = reader.GetBoolean(3),
                        Engine_capacity = reader.GetInt32(4),
                        Manufacture_year = reader.GetInt32(5),
                        Notes = reader.GetString(6),
                        Employee = reader.GetInt32(7),
                        Car_care_date = reader.GetDateTime(8),
                        Edit_date = reader.GetDateTime(9)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

    }
}
