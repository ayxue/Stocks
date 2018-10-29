using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qin.Html;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Data.Services.Sina.RefData;

namespace Trading.Data.Test.Services
{
    [TestClass]
    public class InstrumentServiceTest
    {
        [TestMethod]
        public void _01_InstrumentServiceTest_GetInstruments()
        {
            var html = new HtmlService();
            var service = new InstrumentService(html);
            var instruments = service.GetInstruments();

            Assert.IsTrue(instruments.Length > 2000);
        }
    }
}
