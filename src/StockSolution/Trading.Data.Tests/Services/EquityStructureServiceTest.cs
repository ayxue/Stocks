using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qin.Html;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Data.Services.Sina.Trade;
using Trading.Data.Services.Sina.RefData;

namespace Trading.Data.Test.Services
{
    [TestClass]
    public class EquityStructureServiceTest
    {
        [TestMethod]
        public void _01_EquityStructureServiceTest()
        {
            var html = new HtmlService();
            var service = new EquityStructureService(html);
            var structures = service.GetEquityStructure("600030");

            Assert.IsTrue(structures.Count() > 0);
        }

        
    }
}
