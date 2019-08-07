using Chatbot.Unit.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Chatbot.Unit.Services
{
    public class AuthorizationService
    {

        public void GetAccessToken(BaiduSession session)
        {
            var client = GetClient(session);

            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", session.ClientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", session.ClientSecret));

            var response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            try
            {
                response.EnsureSuccessStatusCode();
                JsonConvert.PopulateObject(result, session);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message + ":" + result);
            }
        }


        private HttpClient GetClient(BaiduSession session)
        {
            if(!session.IsInternal)
                return new HttpClient();

            return new HttpClient(new HttpClientHandler()
            {
                Proxy = new WebProxy("http://sg.pachost.mid.dom")
                {
                    UseDefaultCredentials = true
                }
            });
        }


    }
}
