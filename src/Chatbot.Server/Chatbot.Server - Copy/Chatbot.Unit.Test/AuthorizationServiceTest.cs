using System;
using Chatbot.Unit.Model;
using Chatbot.Unit.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chatbot.Unit.Test
{
    [TestClass]
    public class AuthorizationServiceTest
    {
        [TestMethod]
        public void _01_ShouldGetAccessToken()
        {
            var service = new AuthorizationService();
            var session = BaiduSession.CreateDefault();

            service.GetAccessToken(session);

            Assert.IsFalse(string.IsNullOrEmpty(session.AccessToken));
        }
    }
}
