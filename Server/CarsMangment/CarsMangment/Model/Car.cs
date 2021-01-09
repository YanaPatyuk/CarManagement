using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CarsMangment.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string License_plate { get; set; }
        public int Car_type { get; set; }
        public bool Fourdb {  get;set;}
        public int ?Engine_capacity { get; set; }
        public int Manufacture_year { get; set; }
        public string ?Notes { get; set; }
        public int ?Employee { get; set; }
        public DateTime Car_care_date { get; set; }
        public DateTime Edit_date { get; set; }

        //datebase connection
        internal Database Db { get; set; }


        public Car()
        {
        }

        internal Car(Database db)
        {
            Db = db;
        }



        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `carsdb`.`car` (`license_plate`,`car_type`,`fourdb`,`engine_capacity`,`manufacture_year`,`notes`,`employee`,`car_care_date`,`edit_date`) VALUES (@license_plate ,@car_type ,@fourdb ,@engine_capacity ,@manufacture_year ,@notes ,@employee ,@car_care_date ,@edit_date );";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `carsdb`.`car `SET `id` =  @id , `license_plate` =  @license_plate , `car_type` =  @car_type , `fourdb` =  @fourdb ,`engine_capacity` =  @engine_capacity ,`manufacture_year` =  @manufacture_year ,`notes` =  @notes ,`employee` =  @employee ,`car_care_date` =  @car_care_date ,`edit_date` =  @edit_date  WHERE  `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `carsdb`.`car` WHERE `id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@license_plate",
                DbType = DbType.Int32,
                Value = License_plate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@car_type",
                DbType = DbType.Int16,
                Value = Car_type,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@fourdb",
                DbType = DbType.String,
                Value = Fourdb,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@engine_capacity",
                DbType = DbType.Int16,
                Value = Engine_capacity,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@manufacture_year",
                DbType = DbType.Int16,
                Value = Manufacture_year,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@notes",
                DbType = DbType.String,
                Value = Notes,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@employee",
                DbType = DbType.Int16,
                Value = Employee,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@car_care_date",
                DbType = DbType.DateTime,
                Value = Car_care_date,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@edit_date",
                DbType = DbType.DateTime,
                Value = Edit_date,
            });

        }

    }
}
