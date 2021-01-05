using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using SurfvejrAPI.Models;
using Newtonsoft.Json;
using System.Text.Json;
using SurfvejrAPI.Models.OpenWeather;
using Microsoft.EntityFrameworkCore;



namespace SurfvejrAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILogger<WeatherForecastController> _logger { get; set; }
        private CityDbContext _db { get; set; }
        private IHttpClientFactory _factory { get; set; }
        private const string apiKey = "b7985a93685840905503171f8795df40";

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory factory, CityDbContext cityDbContext)
        {
            _logger = logger;
            _factory = factory;
            _db = cityDbContext;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetWeather(int id)
        {
            City city = _db.Cities.Include(x => x.coordinates).FirstOrDefault(x => x.id == id);
            if (city != null)
            {
                string jsonres = await _factory.CreateClient().GetStringAsync($"https://api.openweathermap.org/data/2.5/onecall?lat={city.coordinates.lat}&lon={city.coordinates.lon}&appid={apiKey}");
                WeatherData weather = JsonConvert.DeserializeObject<WeatherData>(jsonres);

                DateTimeOffset dt = DateTimeOffset.UnixEpoch.AddSeconds(weather.current.dt);
                DateTime current = dt.DateTime;

                WeatherResult weatherResult = new WeatherResult()
                {

                    AirTempC = weather.current.temp,
                    WindSpeed = weather.current.wind_speed,
                    Time = current

                    //Add multiple parameters

                };
                return Ok(weatherResult);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{name}")]
        // Ustabil, danske bogstaver som ÆØÅ virker på nogens requests
        public async Task<IActionResult> GetWeather(string name)
        {
            City city = _db.Cities.Include(x => x.coordinates).FirstOrDefault(x => x.name.ToUpper().Contains(name.ToUpper()));
            if (city != null)
            {
                string jsonres = await _factory.CreateClient().GetStringAsync($"https://api.openweathermap.org/data/2.5/onecall?lat={city.coordinates.lat}&lon={city.coordinates.lon}&appid={apiKey}");
                WeatherData weather = JsonConvert.DeserializeObject<WeatherData>(jsonres);

                DateTimeOffset dt = DateTimeOffset.UnixEpoch.AddSeconds(weather.current.dt);
                DateTime current = dt.DateTime;

                WeatherResult weatherResult = new WeatherResult()
                {

                    AirTempC = weather.current.temp,
                    WindSpeed = weather.current.wind_speed,
                    Time = current
                    //Add multiple parameters

                };
                return Ok(weatherResult);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult PostWeatherForecast([FromBody] List<City> cities)
        {
            foreach (City c in cities)
            {
                if (_db.Cities.FirstOrDefault(x => x.id == c.id) == null)
                {
                    _db.Cities.Add(c);
                }
            }
            _db.SaveChanges();
            return Ok();
        }
    }
}