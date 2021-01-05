using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurfvejrAPI.Models
{
    public class WeatherResult
    {
        public float WaveHeight { get; set; }
        public float WavePeriod { get; set; }
        public float WindSpeed { get; set; }
        public float WaterTempC { get; set; }
        public float AirTempC { get; set; }
        public float WaterTempF => 32 + (int)(WaterTempC / 0.5556);
        public float AirTempF => 32 + (int)(AirTempC / 0.5556);
        public DateTime Time { get; set; }
    }

}
