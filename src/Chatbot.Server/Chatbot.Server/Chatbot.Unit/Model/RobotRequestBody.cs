using Newtonsoft.Json;

namespace Chatbot.Unit.Model
{
    public class RobotRequestBody
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}