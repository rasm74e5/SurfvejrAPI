using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurfvejrAPI.Models
{
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public virtual Coordinates coordinates { get; set; }
    }

    public class Coordinates
    {
        public int id { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
    }

}