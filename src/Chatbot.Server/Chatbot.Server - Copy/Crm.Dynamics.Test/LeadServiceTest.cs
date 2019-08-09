using Crm.Dynamics.Models;
using Crm.Dynamics.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Dynamics.Test
{
    [TestClass]
    public class LeadServiceTest
    {
        [TestMethod]
        public void _01_ShouldGetLeads()
        {
            var session = CrmSession.CreateDefault();
            var service = new TokenService();
            var authResult = service.GetAccessToken(session);
            Assert.IsFalse(string.IsNullOrEmpty(authResult.AccessToken));

            var leadService = new LeadService();
            var leads = leadService.GetLeads(session);
            Assert.IsTrue(leads.Count() > 0);
        }


        [TestMethod]
        public void _02_CreateLeads()
        {
            var session = CrmSession.CreateDefault();
            var service = new TokenService();
            var authResult = service.GetAccessToken(session);
            Assert.IsFalse(string.IsNullOrEmpty(authResult.AccessToken));

            var lead = new LeadModel
            {
                Firstname = "Morten",
                Lastname = "Stanley",
                Emailaddress1 = "morten.stanley@morganstanley.com",
                Mobilephone = "13917163120",
                Telephone1 = "8485000"
            };

            var leadService = new LeadService();
            leadService.CreateLead(lead, session);
            Assert.IsFalse(string.IsNullOrEmpty(lead.CreatedLeadGuid));
        }
    }
}
