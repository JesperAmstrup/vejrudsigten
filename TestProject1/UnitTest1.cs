using System;
using Xunit;
using Vejrudsigten;
using Vejrudsigten.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestProject1
{
    
    public class UnitTest1
    {        
        [Theory]
        [InlineData("Sne", -100, "Sne", -20, "Nok på tide at bruge vinter jakken")]
        [InlineData("Sne", -20, "Sne", -100, "Blot det sædvanlige kedelige vejr")]
        [InlineData("Sne", 0, "Andet", 0, "Så kan man ikke se ukrudet mere")]
        [InlineData("Regn", 5, "Skyet", 5, "Tja …… hvad er der på Netflix")]
        [InlineData("Skyet", 12, "Klart vejr", 20, "Det regner i det mindste ikke")]
        [InlineData("Andet", 20, "Regn", 12, "Ustadig vejr, men ellers skønt")]
        [InlineData("Klart vejr", 28, "Klart vejr", 28, "Hedebølgen fortsætter, orker det ikke mere")]
        [InlineData("Klart vejr", 100, "Klart vejr", 100, "Hedebølgen fortsætter, orker det ikke mere")]
        public async Task WeatherForcastTestAsync(string TodayWeatherType, double TodayTemp, string YesterdayWeatherType, double YesterdayTemp, string ExpectedResult)
        {
            WeatherForecast.service = new WeatherServiceTest()
            {
                TodayWeatherType = TodayWeatherType,
                YesterdayWeatherType = YesterdayWeatherType,
                TodayTemp = TodayTemp,
                YesterdayTemp = YesterdayTemp
            };

            string result = await Vejrudsigten.Services.WeatherForecast.GetForecastAsync("");

            Assert.Equal(ExpectedResult, result);
        }

        [Theory]
        [InlineData("Ligegyldig", -101, "Ligegyldig", 0, "Ligegyldig")]
        [InlineData("Ligegyldig", 101, "Ligegyldig", 0, "Ligegyldig")]
        [InlineData("Ligegyldig", 0, "Ligegyldig", -101, "Ligegyldig")]
        [InlineData("Ligegyldig", 0, "Ligegyldig", 101, "Ligegyldig")]
        [InlineData("Forkert", 0, "Sne", 0, "Ligegyldig")]
        [InlineData("Sne", 0, "Forkert", 0, "Ligegyldig")]
        public async Task WeatherForcastTestExceptionAsync(string TodayWeatherType, double TodayTemp, string YesterdayWeatherType, double YesterdayTemp, string ExpectedResult)
        {
            WeatherForecast.service = new WeatherServiceTest()
            {
                TodayWeatherType = TodayWeatherType,
                YesterdayWeatherType = YesterdayWeatherType,
                TodayTemp = TodayTemp,
                YesterdayTemp = YesterdayTemp
            };

            await Assert.ThrowsAsync<ArgumentException>(() => Vejrudsigten.Services.WeatherForecast.GetForecastAsync(key));
        }


    }

    public class WeatherServiceTest : IWeatherService
    {
        public string YesterdayWeatherType { get; set; }
        public double YesterdayTemp { get; set; }
        public string TodayWeatherType { get; set; }
        public double TodayTemp { get; set; }

        public async Task<WeatherInfo> GetTodaysWeather(string key, string location)
        {
            await Task.Delay(100);
            return new WeatherInfo()
            {
                Conditions = TodayWeatherType,
                Temperature = TodayTemp
            };
        }

        public async Task<WeatherInfo> GetYesterdaysWeather(string key, string location)
        {
            await Task.Delay(100);
            return new WeatherInfo()
            {
                Conditions = YesterdayWeatherType,
                Temperature = YesterdayTemp
            };
        }
    }
}
