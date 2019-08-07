using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Unit.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IntentActionType
    {
        [EnumMember(Value = "satisfy")]
        Satisfy,

        [EnumMember(Value = "failure")]
        Failure,

        [EnumMember(Value = "guide")] 
        Guide
    }
}
