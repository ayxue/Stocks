using System;
using Crm.Dynamics.Models;
using Crm.Dynamics.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crm.Dynamics.Test
{
    [TestClass]
    public class TokenServiceTest
    {
        [TestMethod]
        public void _01_ShouldGetToken()
        {
           var service = new TokenService();
           var authResult = service.GetAccessToken(CrmSession.CreateDefault());
           Assert.IsFalse(string.IsNullOrEmpty(authResult.AccessToken));
        }
    }
}
