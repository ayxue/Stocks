using Crm.Dynamics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Dynamics.Services
{
    public class LeadService
    {
        public LeadModel[] GetLeads(CrmSession session)
        {
            var client = GetClient(session);
            var leadUrl = "https://saxohack.api.crm11.dynamics.com/api/data/v9.1/leads";
            var getLeadsResponse = client.GetAsync(leadUrl).Result; 
            var leadListRawJsonString = getLeadsResponse.Content.ReadAsStringAsync().Result;

            var leads = JsonConvert.DeserializeObject<RootObject<LeadModel>>(leadListRawJsonString).Value;
            return leads;
        }

        public void CreateLead(LeadModel lead, CrmSession session)
        {
            var leadUrl = "https://saxohack.api.crm11.dynamics.com/api/data/v9.1/leads";
            var client = GetClient(session);
            var leadString = JsonConvert.SerializeObject(lead, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            HttpContent leadJsonContent = new StringContent(leadString, Encoding.UTF8, "application/json");
            var createLeadResponse = client.PostAsync(leadUrl, leadJsonContent).Result;

            lead.CreatedLeadGuid = createLeadResponse.Headers.Location.Segments[4];
            lead.LeadId = lead.CreatedLeadGuid.Substring(4, lead.CreatedLeadGuid.Length - 6);
        }


        private HttpClient GetClient(CrmSession session)
        {
            HttpClient client;
            if (!session.IsInternal)
                client = new HttpClient();
            else
            {
                client = new HttpClient(new HttpClientHandler()
                {
                    Proxy = new WebProxy("http://sg.pachost.mid.dom")
                    {
                        UseDefaultCredentials = true
                    }
                });
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AuthenticationResult.AccessToken);
            return client;
        }

    }
}
