using System;
using Xunit;
using Vejrudsigten;

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
        public void WeatherForcastTest(string TodayWeatherType, double TodayTemp, string YesterdayWeatherType, double YesterdayTemp, string ExpectedResult)
        {
            var result = Vejrudsigten.Services.WeatherForecast.GetWeatherForecastMessage(TodayWeatherType, TodayTemp, YesterdayWeatherType, YesterdayTemp);

            Assert.Equal(ExpectedResult, result);
        }
    }
}
