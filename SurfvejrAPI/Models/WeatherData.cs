using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurfvejrAPI.Models.OpenWeather
{
    public class WeatherData
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public Current current { get; set; }
        public Daily[] daily { get; set; }

    }
}
