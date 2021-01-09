using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsMangment.Model
{
    public class BaseCar
    {
        public string License_plate { get; set; }
        public int Car_type { get; set; }
        public bool Fourdb { get; set; }
        public int? Engine_capacity { get; set; }
        public int? Employee { get; set; }
    }
}
