using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Trading.Data.Tests
{
    [TestClass]
    public class JsonTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var app = new App();
            app.Flow = "Code";

            
            var str = JsonConvert.SerializeObject(app, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

        }
    }
}
