using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qin.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Data.Tests.Services
{

    [TestClass]
    public class CategorizationServiceTest
    {
        [TestMethod]
        public void _01_CategorizationServiceTest()
        {
            var html = new HtmlService();
            var service = new CategorizationService(html);
            var categories = service.GetCateogories("600030");

            Assert.IsTrue(categories.Count() > 0);
        }

        [TestMethod]
        public void _02_CategorizationServiceTest()
        {
            var html = new HtmlService();
            var service = new CategorizationService(html);
            var categories = service.GetCateogories("600519");

            Assert.IsTrue(categories.Count() > 0);
        }


    }
}
