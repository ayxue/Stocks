using Chatbot.Unit.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Unit.Services
{
    public class ChatService
    {
        public ChatResult Answer(string robotId, string query, BaiduSession session)
        {
            string token = session.AccessToken;
            string host = "https://aip.baidubce.com/rpc/2.0/unit/service/chat?access_token=" + token;
            var request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.ContentType = "application/json";
            request.KeepAlive = true;

            var robotRequest = RobotRequest.Create(robotId, query, session.SesstionKey);
            var str = JsonConvert.SerializeObject(robotRequest);
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            request.ContentLength = buffer.Length;

            if (session.IsInternal)
            {
                request.Proxy = new WebProxy("http://sg.pachost.mid.dom")
                {
                    UseDefaultCredentials = true
                };
            }

            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            return ToAskResult(result);
        }

        private ChatResult ToAskResult(string result)
        {
            var root = JsonConvert.DeserializeObject(result) as JObject;
            var ret = root.GetValue("result").ToObject<ChatResult>();
            var respList = root.GetValue("result").Value<JArray>("response_list");

            ret.Actions = ParseActions(respList).SelectMany(a => a).ToArray();

            return ret;
        }


        private IEnumerable<IntentAction[]> ParseActions(JArray respList)
        {
            var actions = new List<IntentAction>();
            foreach (JObject response in respList)
            {
                var actionList = response.Value<JArray>("action_list");
                var jsonSchema = response.Value<JObject>("schema");
                foreach (JObject jsonAction in actionList)
                {
                    var action = jsonAction.ToObject<IntentAction>();
                    actions.Add(action);

                    SetTags(jsonSchema, action);
                    SetOptions(jsonAction, action);
                }

                
                foreach (var action in actions)
                    action.Origin = response.Value<string>("origin");

                yield return actions.ToArray();
            }
        }


        private void SetTags(JObject jsonSchema, IntentAction action)
        {
            if(!jsonSchema.ContainsKey("slu_tags"))
            {
                action.Tags = Enumerable.Empty<string>().ToArray();
                return;
            }

            var tags = jsonSchema.Value<JArray>("slu_tags");
            action.Tags = tags.Select(t => t.ToString()).ToArray();
        }


        private void SetOptions(JObject jsonAction, IntentAction action)
        {
            // Options
            var refine = jsonAction.GetValue("refine_detail") as JObject;
            if (refine == null)
                return;

            var options = refine.GetValue("option_list") as JArray;
            if (options == null)
                return;

            foreach (JObject jsonOption in options)
            {
                var optionText = jsonOption.Value<string>("option");
                var option = new IntentOption() { Text = optionText };
                if (action.Options == null)
                    action.Options = new List<IntentOption>();
                action.Options.Add(option);

                action.Say = action.Say + "\n" + optionText;
            }
        }


    }
}
