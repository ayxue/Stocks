using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trading.Data.Services.Sina;
using Trading.Data.Services;
using System.Linq;

namespace Trading.Data.Test
{
    [TestClass]
    public class AutoCheck
    {
        [TestMethod]
        public void _01_CheckProductList()
        {
            var repo = new CnReporsitory();
            var list = repo.GetListFromFile().ToArray();

            var webMeta = new ListService().GetResult(1);
            Assert.AreEqual(webMeta.Count, list.Count(), string.Format("More or less products in the file. File: {0}, Remote: {1}", list.Count(), webMeta.Count));
        }


    }
}
