using CarsMangment.Const;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CarsMangment.Model
{
    public class Car : BaseCar
    {
        public int ?Id { get; set; }
        public int ManufactureYear { get; set; }
        public string ?Notes { get; set; }
        public DateTime CarCareDate { get; set; }
        public DateTime EditDate { get; set; }
        public int ?carTypeId { get; set; }
        public int ?carEmployeeId { get; set; }


        //datebase connection
        internal Database Db { get; set; }


        public Car()
        {
        }

        internal Car(Database db)
        {
            Db = db;
        }

        public void Copy(Car other)
        {
            this.Id = other.Id;
            this.LicensePlate = other.LicensePlate;
            this.CarType = other.CarType;
            this.Fourdb = other.Fourdb;
            this.EngineCapacity = other.EngineCapacity;
            this.ManufactureYear = other.ManufactureYear;
            this.Notes = other.Notes;
            this.EmployeeFirstName = other.EmployeeFirstName;
            this.EmployeeLastName = other.EmployeeLastName;
            this.CarCareDate = other.CarCareDate;
            this.EditDate = other.EditDate;
            this.carEmployeeId = other.carEmployeeId;
            this.carTypeId = other.carTypeId;
        }



        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = SqlQuerys.InsertNewCar;
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
            //add car type to car type table
            cmd.CommandText = SqlQuerys.InsertNewCarType;
            BindTypeParam(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = SqlQuerys.UpdateCar;
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
            //add car type to car type table
            cmd.CommandText = SqlQuerys.InsertNewCarType;
            BindTypeParam(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = SqlQuerys.DeleteCar;
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = this.Id,
            });
        }
        private void BindTypeParam(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@type_name",
                DbType = DbType.String,
                Value = this.CarType,
            });
        }
        private void BindParams(MySqlCommand cmd)
        {

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@license_plate",
                DbType = DbType.String,
                Value = this.LicensePlate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@car_type",
                DbType = DbType.Int16,
                Value = this.carTypeId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@fourdb",
                DbType = DbType.String,
                Value = this.Fourdb,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@engine_capacity",
                DbType = DbType.Int16,
                Value = this.EngineCapacity,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@manufacture_year",
                DbType = DbType.Int16,
                Value = this.ManufactureYear,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@notes",
                DbType = DbType.String,
                Value = this.Notes,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@employee",
                DbType = DbType.Int16,
                Value = this.carEmployeeId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@car_care_date",
                DbType = DbType.DateTime,
                Value = this.CarCareDate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@edit_date",
                DbType = DbType.DateTime,
                Value = this.EditDate,
            });

        }

    }
}
