using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trading.Data.Services.Sina;
using Trading.Data.Services;
using System.Linq;
using System.Net;
using Qin.Html;
using Trading.Data.Services.Sina.Trade;

namespace Trading.Data.Test.Services
{
    [TestClass]
    public class TradingInfoServiceTest
    {
        private static readonly string Code = "002352";

        /// <summary>
        /// Failed if URL not available
        /// </summary>
        [TestMethod]
        public void _01_DailyTradeTest()
        {
            var service = new PriceHistoryService(new HtmlService());
            var years = service.GetAvailableYears(Code).ToArray();

            int beginYear = 2010;
            int endYear = DateTime.Today.Year;
            Assert.AreEqual((endYear - beginYear + 1), years.Count());

        }

        [TestMethod]
        public void _02_DailyTradePeriodTest()
        {
            var service = new PriceHistoryService(new HtmlService());
            var years = service.GetAvailablePeriods(Code).ToArray();

            var count = Convert.ToInt32(Math.Ceiling(DateTime.Today.Month / 3.0));
            Assert.AreEqual(count, years.Count());
        }


        [TestMethod]
        public void _03_DailyTradePeriodTest()
        {
            var service = new PriceHistoryService(new HtmlService());
            var pairs = service.GetAvailablePeriods(Code, new DateTime(2017, 6, 1)).ToArray();

            int beginYear = 2010;
            int endYear = 2017;
            Assert.AreEqual((endYear - beginYear) * 4 + 2, pairs.Count());
        }

        [TestMethod]
        public void _04_DailyTradePeriodTest()
        {
            var service = new PriceHistoryService(new HtmlService());
            var pairs = service.GetAvailablePeriods(Code, new DateTime(2017, 12, 31), new DateTime(2017, 1, 1)).ToArray();

            int beginYear = 2017;
            int endYear = 2017;
            Assert.AreEqual(4, pairs.Count());
        }


        [TestMethod]
        public void _05_DailyTradePeriodTest()
        {
            var service = new PriceHistoryService(new HtmlService());
            var pairs = service.GetAvailablePeriods(Code, new DateTime(2018, 6, 27), new DateTime(2017, 4, 5)).ToArray();

            Assert.AreEqual(5, pairs.Count());
        }

        [TestMethod]
        public void _06_DailyTradeInfoTest()
        {
            var service = new PriceHistoryService(new HtmlService());
            var info = service.GetDailyTadingInfo(Code, 2018, 1).ToArray();
        }

    }
}
