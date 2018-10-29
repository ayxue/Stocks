using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qin.Html;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Data.Services.Sina.Trade;

namespace Trading.Data.Test.Services
{
    [TestClass]
    public class HistoricalPriceServiceTest
    {
        [TestMethod]
        public void _01_HistoricalPriceServiceTest()
        {
            var html = new HtmlService();
            var service = new PriceHistoryService(html);
            var prices = service.GetDailyTadingInfo("600030", 2018, 3).ToArray();

            Assert.IsTrue(prices.Length > 0);
        }


        [TestMethod]
        public void _02_HistoricalPriceServiceTest_ByDate()
        {
            var begin = new DateTime(2017, 5, 9);
            var end = new DateTime(2017, 5, 9);
            var html = new HtmlService();
            var service = new PriceHistoryService(html);

            // test Begin
            var prices = service.GetDailyTadingInfo("600030", begin).ToArray();
            Assert.IsTrue(prices.Length > 0);
            Assert.IsTrue(prices.Min(p => p.Time) == begin);

            // test end
            //prices = service.GetDailyTadingInfo("600030", null, end).ToArray();
            //Assert.IsTrue(prices.Length > 0);
            //Assert.IsTrue(prices.Max(p => p.Time) == begin);

            // test begin + end
            prices = service.GetDailyTadingInfo("600030", begin, begin).ToArray();
            Assert.IsTrue(prices.Length == 1);
            Assert.IsTrue(prices.Min(p => p.Time) == begin);
            Assert.IsTrue(prices.Max(p => p.Time) == begin);

            prices = service.GetDailyTadingInfo("600030", begin, end.AddMonths(7)).ToArray();
            Assert.IsTrue(prices.Length > 1);
            Assert.IsTrue(prices.Min(p => p.Time) == begin);
            Assert.IsTrue(prices.Max(p => p.Time) == end.AddMonths(7));
        }
    }
}
