using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsMangment.Model
{
    public class BaseCar
    {
        public string LicensePlate { get; set; }
        public string CarType { get; set; }
        public bool Fourdb { get; set; }
        public int? EngineCapacity { get; set; }
        public string? EmployeeFirstName { get; set; }
        public string? EmployeeLastName { get; set; }
    }
}
