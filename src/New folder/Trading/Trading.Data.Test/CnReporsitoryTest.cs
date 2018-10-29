using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trading.Data.Services.Sina;
using Trading.Data.Services;
using System.Linq;

namespace Trading.Data.Test
{
    [TestClass]
    public class CnReporsitoryTest
    {
        /// <summary>
        /// Failed products in the file is more o less than that on the web
        /// </summary>
        [TestMethod]
        public void _01_CnReporsitoryTest_GetFromFile()
        {
            var repo = new CnReporsitory();
            var list = repo.GetListFromFile().ToArray();

            var webMeta = new ListService().GetResult(1);
            Assert.AreEqual(webMeta.Count, list.Count());
        }

        /// <summary>
        /// Util to download all poduct list from web
        /// </summary>
        [TestMethod]
        [Ignore]
        public void _02_CnReporsitoryTest_GetFromWeb()
        {
            var repo = new CnReporsitory();
            var list = repo.GetListFromWeb().ToArray();

            var str = JsonUtil.Serialize(list);
        }


        [TestMethod]
        public void _03_CnReporsitoryTest_TradeFromWeb()
        {
            var repo = new CnReporsitory();
            var list = repo.GetDailyTadingInfo("600030").ToArray();

            var str = JsonUtil.Serialize(list);
        }

    }
}
