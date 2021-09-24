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
            var todayInfo = await WeatherService.GetTodaysWeather(key, "Kolding");
            var yesterdayInfo = await WeatherService.GetYesterdaysWeather(key, "Kolding");

            String result = "Vejret i Kolding er {0} og der er {1} grader. I går var det {2} og {3} grader";
            return String.Format(result, todayInfo.Conditions, todayInfo.Temperature, yesterdayInfo.Conditions, yesterdayInfo.Temperature);
        }
    }
}
