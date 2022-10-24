using System.Threading.Tasks;

namespace Vejrudsigten.Services
{
    public interface IWeatherService
    {
        public Task<WeatherInfo> GetTodaysWeather(string key, string location);
        public Task<WeatherInfo> GetYesterdaysWeather(string key, string location);
    }
}
