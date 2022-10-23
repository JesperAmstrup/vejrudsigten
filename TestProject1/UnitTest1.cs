using System;
using Xunit;
using Vejrudsigten;

namespace TestProject1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("Sne", -100, "Sne", -20, "Nok p� tide at bruge vinter jakken")]
        [InlineData("Sne", -20, "Sne", -100, "Blot det s�dvanlige kedelige vejr")]
        [InlineData("Sne", 0, "Andet", 0, "S� kan man ikke se ukrudet mere")]
        [InlineData("Regn", 5, "Skyet", 5, "Tja �� hvad er der p� Netflix")]
        [InlineData("Skyet", 12, "Klart vejr", 20, "Det regner i det mindste ikke")]
        [InlineData("Andet", 20, "Regn", 12, "Ustadig vejr, men ellers sk�nt")]
        [InlineData("Klart vejr", 28, "Klart vejr", 28, "Hedeb�lgen forts�tter, orker det ikke mere")]
        [InlineData("Klart vejr", 100, "Klart vejr", 100, "Hedeb�lgen forts�tter, orker det ikke mere")]
        public void WeatherForcastTest(string TodayWeatherType, double TodayTemp, string YesterdayWeatherType, double YesterdayTemp, string ExpectedResult)
        {
            var result = Vejrudsigten.Services.WeatherForecast.GetWeatherForecastMessage(TodayWeatherType, TodayTemp, YesterdayWeatherType, YesterdayTemp);

            Assert.Equal(ExpectedResult, result);
        }
    }
}
