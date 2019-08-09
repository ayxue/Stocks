using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Unit.Model
{
    public class RobotRequest
    {
        public static RobotRequest Create(string robotId, string query, string sessionId)
        {
            return new RobotRequest()
            {
                LogId = sessionId,
                ServiceId = robotId,
                SessionId = sessionId,
                Request = new RobotRequestBody
                {
                    Query = query,
                    UserId = sessionId
                }
            };
        }

        [JsonProperty("log_id")]
        public string LogId { get; set; }

        [JsonProperty("version")]
        public string Version => "2.0";

        [JsonProperty("service_id")]
        public string ServiceId { get; protected set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("request")]
        public RobotRequestBody Request { get; set; }

        [JsonProperty("dialog_state")]
        public DialogState DialogState { get; set; }

    }
}
