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

        public async Task<Car> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `carsdb`.`car` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Car>> AllCarsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `carsdb`.`car`;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        
        private async Task<List<Car>> ReadAllAsync(DbDataReader reader)
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
