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
    public class InfoPriceServiceTest
    {
        [TestMethod]
        public void _01_TestInfoPrice()
        {
            var htmlService = new HtmlService();
            var infoPriceService = new PriceService(htmlService);
            var ret = infoPriceService.GetInfoPrices("sh600030", "sh600001").ToArray();
        }



    }
}
