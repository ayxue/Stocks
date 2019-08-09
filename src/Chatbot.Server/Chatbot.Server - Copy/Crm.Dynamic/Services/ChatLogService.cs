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
    public class ChatLogService
    {
        public void CreateChatLog(ChatLogModel chatLog, CrmSession session)
        {
            var chatLogUrl = "https://saxohack.api.crm11.dynamics.com/api/data/v9.1/new_chatlogs";
            var client = GetClient(session);
            var chatLogString = JsonConvert.SerializeObject(chatLog, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var chatLogJsonContent = new StringContent(chatLogString, Encoding.UTF8, "application/json");
            var createChatLogResponse = client.PostAsync(chatLogUrl, chatLogJsonContent).Result;
            var msg = createChatLogResponse.Content.ReadAsStringAsync().Result;

            try
            {
                createChatLogResponse.EnsureSuccessStatusCode();
                chatLog.CreatedChatLogGuid = createChatLogResponse.Headers.TryGetValues("OData-EntityId", out var chatLogCreationheaders) ? chatLogCreationheaders.FirstOrDefault() : null;
            }
            catch(Exception ex)
            {
                throw new Exception(msg, ex);
            }
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
