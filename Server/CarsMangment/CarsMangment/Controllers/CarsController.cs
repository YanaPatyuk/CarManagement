using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsMangment.Model;
using Microsoft.AspNetCore.Mvc;



//https://mysqlconnector.net/tutorials/net-core-mvc/
namespace CarsMangment.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        public Database Db { get; }

        public CarsController(Database db)
        {
            Db = db;
        }

        // GET api/cars
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            await Db.Connection.OpenAsync();
            var query = new DatabaseQuery(Db);
            var result = await query.AllCarsAsync();
            return new OkObjectResult(result);
        }

        // GET api/cars/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneCar(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new DatabaseQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }
    }
}
