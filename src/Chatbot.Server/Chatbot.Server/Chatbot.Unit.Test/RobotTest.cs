using System;
using Chatbot.Unit.Model;
using Chatbot.Unit.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chatbot.Unit.Test
{
    [TestClass]
    public class RobotTest
    {
        [TestMethod]
        public void _01_ShouldGetResponse()
        {
            var accessToken = "24.3ffc02b72fb1f4b04b05bf67fb0b4e0d.2592000.1567668766.282335-16961428";
            var robot = new ChatService();
            var ret = robot.Answer("S20981", "你好", new BaiduSession { AccessToken = accessToken, SesstionKey = "TestSession" });
            Assert.IsFalse(string.IsNullOrEmpty(ret.InteractionId));
        }

    }
}
