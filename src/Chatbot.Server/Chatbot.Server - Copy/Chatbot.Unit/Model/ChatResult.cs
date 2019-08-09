using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Unit.Model
{
    public class ChatResult
    {
        [JsonProperty("timestamp")]
        public string Timestemp { get; set; }

        [JsonProperty("service_id")]
        public string ServiceId { get; set; }

        [JsonProperty("log_id")]
        public string LogId { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("interaction_id")]
        public string InteractionId { get; set; }

        [JsonProperty("action_list")]
        public IntentAction[] Actions { get; set; }

        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
    }
}
