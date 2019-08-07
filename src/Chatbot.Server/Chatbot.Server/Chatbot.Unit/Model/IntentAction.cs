using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Unit.Model
{
    [DebuggerDisplay("{Type} - {Origin} - {Say}")]
    public class IntentAction
    {
        [JsonProperty("origin")]
        public string Origin { get; set; }


        [JsonProperty("action_id")]
        public string ActionId { get; set; }

        [JsonProperty("confidence")]
        public int Confidence { get; set; }

        [JsonProperty("say")]
        public string Say { get; set; }

        [JsonProperty("type")]
        public IntentActionType Type { get; set; }


    }
}
