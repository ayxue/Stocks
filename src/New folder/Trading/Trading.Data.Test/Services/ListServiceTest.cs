using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trading.Data.Services.Sina;
using Trading.Data.Services;
using System.Linq;

namespace Trading.Data.Test.Services
{
    [TestClass]
    public class ListServiceTest
    {
        /// <summary>
        /// Failed if URL not available
        /// </summary>
        [TestMethod]
        public void _01_ListService_HeartBeatRemote()
        {
            var service = new ListService();
            var address = service.GetServiceAddress(1);
            var client = new HttpService();
            var ret = client.Get(address);
        }

        /// <summary>
        /// Failed if error format returned 
        /// </summary>
        [TestMethod]
        public void _02_ListService_SinaResultFormat()
        {
            var service = new ListService();
            var result = service.GetResult(1);
        }

        /// <summary>
        /// Failed if item in error format
        /// </summary>
        [TestMethod]
        public void _03_ListService_ItemFormat()
        {
            var service = new ListService();
            var result = service.GetResult(1);

            for(int i = 0; i < result.Fields.Count(); i ++)
                foreach(var item in result.Items)
                {
                    var fieldName = result.Fields[i];
                    var val = item[i];
                }
        }

    }
}
