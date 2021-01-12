using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsMangment.Const
{
    /*
     * Contains all sql querys to database.
     */
    public class SqlQuerys
    {
        public static readonly string GetAllCarsBaseInfo = @"select c.license_plate, t.type_name,c.engine_capacity, c.fourdb, e.first_name, e.last_name
                                            from car c, car_type t, employee e
                                            where(c.employee = e.id) and(c.car_type = t.id)
                                            union
                                            select c.license_plate, t.type_name,c.engine_capacity, c.fourdb, null as 'fist_name', null as 'last_name'
                                            from `carsdb`.`car` c, `carsdb`.`car_type` t
                                            where(c.employee is null) and (c.car_type = t.id);";

        public static readonly string GetFullCarInfoByLicense = @"select c.id, c.license_plate, c.fourdb, c.engine_capacity, c.manufacture_year, c.notes, c.car_care_date, c.edit_date, c.employee, e.first_name, e.last_name, t.id as 'type_id', t.type_name                                            from car c, car_type t, employee e
                                            where(c.employee = e.id) and(c.car_type = t.id) and (c.license_plate=@license_plate)
                                            union
                                           select c.id, c.license_plate, c.fourdb, c.engine_capacity, c.manufacture_year, c.notes, c.car_care_date, c.edit_date, c.employee, null as 'fist_name', null as 'last_name', t.id as 'type_id', t.type_name
                                            from `carsdb`.`car` c, `carsdb`.`car_type` t
                                            where(c.employee is null) and (c.car_type = t.id) and (c.license_plate=@license_plate);";

        public static readonly string GetFullCarInfoById = @"select c.id, c.license_plate, c.fourdb, c.engine_capacity, c.manufacture_year, c.notes, c.car_care_date, c.edit_date, c.employee, e.first_name, e.last_name, t.id as 'type_id', t.type_name                                            from car c, car_type t, employee e
                                            where(c.employee = e.id) and(c.car_type = t.id) and (c.id=@id)
                                            union
                                            select c.id, c.license_plate, c.fourdb, c.engine_capacity, c.manufacture_year, c.notes, c.car_care_date, c.edit_date, c.employee, null as 'fist_name', null as 'last_name', t.id as 'type_id', t.type_name
                                            from `carsdb`.`car` c, `carsdb`.`car_type` t
                                            where(c.employee is null) and (c.car_type = t.id) and (c.id=@id);";



        public static readonly string InsertNewCar = @"INSERT INTO `carsdb`.`car` (`license_plate`,`car_type`,`fourdb`,`engine_capacity`,`manufacture_year`,`notes`,`employee`,`car_care_date`,`edit_date`) VALUES (@license_plate ,@car_type ,@fourdb ,@engine_capacity ,@manufacture_year ,@notes ,@employee ,@car_care_date ,@edit_date );";
        public static readonly string DeleteCar = @"DELETE FROM `carsdb`.`car` WHERE `id` = @id;";
        public static readonly string UpdateCar = @"UPDATE `carsdb`.`car` SET `id` =  @id , `license_plate` =  @license_plate , `car_type` =  @car_type , `fourdb` =  @fourdb ,`engine_capacity` =  @engine_capacity ,`manufacture_year` =  @manufacture_year ,`notes` =  @notes ,`employee` =  @employee ,`car_care_date` =  @car_care_date ,`edit_date` =  @edit_date  WHERE  `id` = @id;";



        public static readonly string InsertNewCarType = @"INSERT INTO `carsdb`.`car_type` (type_name)
                                                          SELECT * FROM (SELECT @type_name) AS tmp
                                                          WHERE NOT EXISTS (
                                                          SELECT id FROM car_type WHERE type_name = @type_name) LIMIT 1;";
        public static readonly string GetTypeID = @"SELECT id FROM  `carsdb`.`car_type` WHERE type_name = @type_name;";
        public static readonly string GetCarType = @"";
        public static readonly string GetAllCarTypes = @"SELECT * FROM `carsdb`.`car_type`;";

        public static readonly string GetAllEmployees = @"SELECT * FROM `carsdb`.`employee`;";
    }
}
