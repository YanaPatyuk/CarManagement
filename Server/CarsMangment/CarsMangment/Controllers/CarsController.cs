﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsMangment.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

/**
 * Used //https://mysqlconnector.net/tutorials/net-core-mvc/
 * For mysql querys.
 */

namespace CarsMangment.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        public Database Db { get; }//database 

        public CarsController(Database db)
        {
            Db = db;
        }
        // GET api/cars/Types - get car types - return list of types
        [HttpGet("Types")]
        public async Task<IActionResult> GetAllCarTypes()
        {
            Console.WriteLine("Controller: Get all car types");
            await Db.Connection.OpenAsync();
            var query = new DatabaseQuery(Db);
            var result = await query.AllCarTypes();

            return new OkObjectResult(result);
        }
        // GET api/cars/Employees -Return list of employees
        [HttpGet("Employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            Console.WriteLine("Controller: Get all emploees");
            await Db.Connection.OpenAsync();
            var query = new DatabaseQuery(Db);
            var result = await query.AllEmployees();
            return new OkObjectResult(result);
        }
        // GET api/cars - Return List of base cars
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            Console.WriteLine("Controller: Get all cars");
            await Db.Connection.OpenAsync();
            var query = new DatabaseQuery(Db);
            var result = await query.AllCarsAsync();
            return new OkObjectResult(result);
        }


        // GET api/cars/AZ123(example) return one car-full details
        [HttpGet("{license_plate}")]
        public async Task<IActionResult> GetOneCar(string license_plate)
        {
            Console.WriteLine("Controller: Get one car:" + license_plate);
            await Db.Connection.OpenAsync();
            var query = new DatabaseQuery(Db);
            var result = await query.FindOneCarAsync(license_plate);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/cars/AddCar add new car
        [HttpPost("AddCar")]
        public async Task<IActionResult> AddCar([FromBody] Car body)
        {
            Console.WriteLine("Controller: Add car with license plate:" + body.LicensePlate);
            await Db.Connection.OpenAsync();
            //check if already in database - if so, return.
            var query = new DatabaseQuery(Db);
            var result = await query.FindOneCarAsync(body.LicensePlate);
            if (result != null)
                return Content("{\"status\":\"Error License plate in DB!\"}");
            //insert type id to list if needed and return its id
            int type_id = await query.GetTypeID(body.CarType);
            body.carTypeId = type_id;
            //add to db
            body.Db = Db;
            await body.InsertAsync();
            return Content("{\"status\":\"OK\"}");
        }


        // PUT api/cars/5 Update gicen car by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car body)
        {
            Console.WriteLine("Controller: Update car with id:"+ id);
            await Db.Connection.OpenAsync();
            var query = new DatabaseQuery(Db);
            var result = await query.FindOneCarAsync(id);
            if (result is null)
                return new NotFoundResult();
            //check if new licenace is in database - if so, return.
            var result2 = await query.FindOneCarAsync(body.LicensePlate);
            if ((result != null) && result2.Id != result.Id)
                return Content("{\"status\":\"Error License plate in DB!\"}");

            //insert type id to list if needed and return the id of updated type
            int type_id = await query.GetTypeID(body.CarType);
            body.carTypeId = type_id;
            body.Db = Db;
            //copy and update.
            result.Copy(body);
            await result.UpdateAsync();
            return new OkObjectResult("{\"status\":\"OK\"}");
        }

        // DELETE api/cars/5 delete car by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneCar(int id)
        {
            Console.WriteLine("Controller: Delete car with id:" + id);
            await Db.Connection.OpenAsync();
            var query = new DatabaseQuery(Db);
            var result = await query.FindOneCarAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Db = Db;
            await result.DeleteAsync();
            return new OkResult();
        }



    }
}
