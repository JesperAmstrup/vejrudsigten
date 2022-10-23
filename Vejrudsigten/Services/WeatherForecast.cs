using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vejrudsigten.Services
{
    public static class WeatherForecast
    {
        private static string _weatherForecastDefaultMessage = "Blot det sædvanlige kedelige vejr";

        public static async Task<string> GetForecastAsync(string key)
        {
            WeatherService service = new WeatherService();
            var todayInfo = await service.GetTodaysWeather(key, "Kolding");
            var yesterdayInfo = await service.GetYesterdaysWeather(key, "Kolding");

            //String result = "Vejret i Kolding er {0} og der er {1} grader. I går var det {2} og {3} grader";
            //return String.Format(result, todayInfo.Conditions, todayInfo.Temperature, yesterdayInfo.Conditions, yesterdayInfo.Temperature);

            string WeatherMessage = "";
            try
            {
                WeatherMessage = GetWeatherForecastMessage(todayInfo.Conditions, todayInfo.Temperature, yesterdayInfo.Conditions, yesterdayInfo.Temperature);
            }
            catch
            {
                WeatherMessage = _weatherForecastDefaultMessage;
            }

            return WeatherMessage;
        }

        public static string GetWeatherForecastMessage(string TodayWeatherType, double TodayTemp, string YesterdayWeatherType, double YesterdayTemp)
        {
            List<WeatherForecastMessage> WeatherForecastMessages = GetWeatherForecastMessages();

            if (TodayTemp>100 || YesterdayTemp> 100)
            {
                throw new Exception("Bor du på Venus?");
            }

            if (TodayTemp < -100 || YesterdayTemp < -100)
            {
                throw new Exception("Bor du på Uranus?");
            }

            if(!"Alle,Klart vejr,Skyet,Andet,Regn,Sne".Contains(TodayWeatherType))
            {
                throw new Exception("Den vejr type kender jeg ikke: '"+ TodayWeatherType + "'");
            }

            if (!"Alle,Klart vejr,Skyet,Andet,Regn,Sne".Contains(YesterdayWeatherType))
            {
                throw new Exception("Den vejr type kender jeg ikke: '" + YesterdayWeatherType + "'");
            }


            if (WeatherForecastMessages.Where(f => f.TodayTempFrom <= TodayTemp && (f.TodayTempTo > TodayTemp || (f.TodayTempTo == 100 && TodayTemp == 100)) && f.YesterdayTempFrom <= YesterdayTemp && (f.YesterdayTempTo > YesterdayTemp || (f.YesterdayTempTo == 100 && YesterdayTemp == 100)) && (f.YesterdayWeatherType == "Alle" || f.YesterdayWeatherType.Contains(YesterdayWeatherType)) && (f.TodayWeatherType == "Alle" || f.TodayWeatherType.Contains(TodayWeatherType))).Count()>0)
            { 
                return WeatherForecastMessages.Where(f => f.TodayTempFrom <= TodayTemp && (f.TodayTempTo > TodayTemp || (f.TodayTempTo == 100 && TodayTemp == 100)) && (f.YesterdayTempTo > YesterdayTemp || (f.YesterdayTempTo == 100 && YesterdayTemp == 100)) && (f.YesterdayWeatherType == "Alle" || f.YesterdayWeatherType.Contains(YesterdayWeatherType)) && (f.TodayWeatherType == "Alle" || f.TodayWeatherType.Contains(TodayWeatherType))).FirstOrDefault().Message;
            }

            return _weatherForecastDefaultMessage;
        }


        private static List<WeatherForecastMessage> GetWeatherForecastMessages()
        {
            List<WeatherForecastMessage> WeatherForecastMessages = new List<WeatherForecastMessage>();
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", 28, 100, "Alle", 28, 100, "Hedebølgen fortsætter, orker det ikke mere"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", 28, 100, "Alle", 23, 28, "Som om det ikke var varmt nok allerede"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", 28, 100, "Alle", -100, 23, "Køb alovea aktier nu, der er rød flæsk i sigte"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Klart vejr", 20, 28, "Alle", 20, 100, "Nyd et par kolde øl med naboen"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Sne,Skyet,Andet,Regn", 20, 28, "Alle", -100, 100, "Ustadig vejr, men ellers skønt"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", 20, 28, "Alle", -100, 20, "Skynd dig at få købt solcreame og koldskål"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Klart vejr", 20, 28, "Sne, Skyet, Andet, Regn", 20, 28, "Skynd dig at få købt solcreame og koldskål"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Regn", 12, 20, "Alle", 20, 100, "Typisk dansk sommervejr"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Klart vejr,Skyet,Andet", 12, 20, "Alle", 20, 100, "Det regner i det mindste ikke"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", 12, 20, "Alle", 12, 20, "Skønt vejr til en løbetur"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", 12, 20, "Alle", -100, 12, "Går da bedre end i går"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Klart vejr,Skyet,Andet", 5, 12, "Alle", 12, 100, "Det regner i det mindste ikke"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Regn, Sne", 5, 12, "Alle", -100, 100, "Tja …… hvad er der på Netflix"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Klart vejr,Skyet,Andet", 0, 12, "Alle", -100, 12, "Kom ud og nyd det friske vejr"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Regn", 0, 5, "Klart vejr,Skyet,Andet,Sne", -100, 100, "Skod vejr i sigte"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Regn", 0, 5, "Regn", -100, 100, "Samme skod vejr igen i dag"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Sne", 0, 5, "Klart vejr,Skyet,Andet,Regn", -100, 100, "Så kan man ikke se ukrudet mere"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Sne", 0, 5, "Sne", -100, 100, "Så skal det sne nok snart rydes"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", -10, 0, "Alle", 0, 100, "Nu skal tøjet nok ind fra tørresnoren"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", -10, 0, "Alle", -10, 0, "Håber du har fået tøjet ind fra tørresnoren"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", -20, -10, "Alle", -10, 100, "Overvej at få købt en vinter jakken"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", -20, -10, "Alle", -20, -10, "Få nu købt den vinter jakke"));
            WeatherForecastMessages.Add(new WeatherForecastMessage("Alle", -100, -20, "Alle", -100, 100, "Nok på tide at bruge vinter jakken"));

            return WeatherForecastMessages;
        }
    }

    public class WeatherForecastMessage
    {
        public WeatherForecastMessage(string TodayWeatherType, double TodayTempFrom, double TodayTempTo, string YesterdayWeatherType, double YesterdayTempFrom, double YesterdayTempTo, string Message)
        {
            this.TodayWeatherType = TodayWeatherType;
            this.TodayTempFrom = TodayTempFrom;
            this.TodayTempTo = TodayTempTo;
            this.YesterdayWeatherType = YesterdayWeatherType;
            this.YesterdayTempFrom = YesterdayTempFrom;
            this.YesterdayTempTo = YesterdayTempTo;
            this.Message = Message;
        }

        public string TodayWeatherType { get; set; }
        public double TodayTempFrom { get; set; }
        public double TodayTempTo { get; set; }
        public string YesterdayWeatherType { get; set; }
        public double YesterdayTempFrom { get; set; }
        public double YesterdayTempTo { get; set; }
        public string Message { get; set; }
    }
}
