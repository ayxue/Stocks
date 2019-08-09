using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace HackConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var accessToken = "";

            try
            {
                var applicationId = "bece060c-29b8-442c-a3e3-03af050f32d2";
                var clientSecret = "yE81S:sA5-zpgPDo*B7VFceE/DT/=d-9";
                var aadInstanceUrl = "https://login.microsoftonline.com";
                var tenantId = "37378be5-7862-4470-87b2-eb0edbe89793";
                var organizationUrl = "https://saxohack.crm11.dynamics.com/";

                var clientCredential = new ClientCredential(applicationId, clientSecret);
                var authenticationContext = new AuthenticationContext($"{aadInstanceUrl}/{tenantId}");
                var authenticationResult =
                    await authenticationContext.AcquireTokenAsync(organizationUrl, clientCredential);
                accessToken = authenticationResult.AccessToken;

                Console.WriteLine(accessToken);
                Console.WriteLine(authenticationResult.AccessTokenType);
                Console.WriteLine(authenticationResult.ExpiresOn);
                Console.WriteLine("Obtained Access Token.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error obtain access token", ex);
            }


            var proxy = new WebProxy("http://sg.pachost.mid.dom")
            {
                UseDefaultCredentials = true
            };

            var httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy
            };

            var client = new HttpClient(httpClientHandler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            
            var leadUrl = "https://saxohack.api.crm11.dynamics.com/api/data/v9.1/leads";
            var getLeadsResponse = await client.GetAsync(leadUrl); // get all leads from D365, filters can be added
            var leadListRawJsonString = await getLeadsResponse.Content.ReadAsStringAsync();

            var leads = JsonConvert.DeserializeObject<RootObject>(leadListRawJsonString).value;
            Console.WriteLine("{0}, total number of leads obtained", leads.Count);
            
            // User info to be obtained from Chat window
            var lead = new LeadModel
            {
                firstname = "Saxo Chatbot",
                lastname = "Test",
                emailaddress1 = "primary email address",
                mobilephone = "123456789",
                telephone1 = ""
            };

            var leadString = JsonConvert.SerializeObject(lead, Formatting.None, new JsonSerializerSettings{ NullValueHandling = NullValueHandling.Ignore });

            HttpContent leadJsonContent = new StringContent(leadString, Encoding.UTF8, "application/json");
            var createLeadResponse = await client.PostAsync(leadUrl, leadJsonContent);

            var createdLeadGuid = createLeadResponse.Headers.Location.Segments[4];

            Console.WriteLine("Created lead {0}", createdLeadGuid);

            var chatLogUrl = "https://saxohack.api.crm11.dynamics.com/api/data/v9.1/new_chatlogs";

            var chatLog = new ChatLogModel
            {
                SessionId =  "session id xxxx",
                Log = @"再多教它一些东西
                通过对话模板让技能模型学习用户意图的多种表达方式
                也可尽量多地告诉它用户的真实问句（对话样本），同时标出用户的意图和实现意图的关键信息
                对话模型就像个儿童，您教得越多，它越能领会您的意思，而且还能举一反三呢~
                这部分在【效果优化--训练数据】里完成",
                Tags = "100000000,100000001,100000002,100000003",
                LeadIdOdataBind = createdLeadGuid
            };

            var chatLogString = JsonConvert.SerializeObject(chatLog, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            HttpContent chatLogJsonContent = new StringContent(chatLogString, Encoding.UTF8, "application/json");
            var createChatLogResponse = await client.PostAsync(chatLogUrl, chatLogJsonContent);

            var createdChatLogGuid = createChatLogResponse.Headers.TryGetValues("OData-EntityId", out var chatLogCreationheaders) ? chatLogCreationheaders.FirstOrDefault() : null;

            Console.WriteLine("Created chat log {0}", createdChatLogGuid);

            Console.ReadLine();
        }
    }
}
