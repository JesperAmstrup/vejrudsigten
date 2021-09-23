using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vejrudsigten.Services
{
    public static class WeatherForecast
    {
        public static async Task<string> GetForecastAsync(string key)
        {
            var weatherInfo = await WeatherService.GetTodaysWeather(key, "Kolding");
            
            String result = "Vejret i Kolding er {0} og der er {1} grader";
            return String.Format(result, weatherInfo.Conditions, weatherInfo.Temperature);
        }
    }
}
