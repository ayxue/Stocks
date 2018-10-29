using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qin.Html;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Data.Services.Sina.Trade;
using Trading.Data.Services.Sina.RefData;
using Trading.Data.Services.Sina.Finance;

namespace Trading.Data.Test.Services
{
    [TestClass]
    public class PnlTableServiceTest
    {
        [TestMethod]
        public void _01_PnlTableServiceTest()
        {
            var html = new HtmlService();
            var service = new PnlService(html);
            var pnl = service.GetPnlTable("600030", 2018);

            Assert.IsTrue(pnl.Count() > 0);
        }


        [TestMethod]
        public void _02_PnlTableServiceTest()
        {
            var html = new HtmlService();
            var service = new PnlService(html);
            var years = service.GetYears("600030");

            Assert.IsTrue(years.Count() > 0);
        }

    }
}
