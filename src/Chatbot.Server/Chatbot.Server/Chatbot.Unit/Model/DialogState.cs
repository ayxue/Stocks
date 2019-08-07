using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chatbot.Unit.Model
{
    public class DialogState
    {
        public DialogState()
        {
            Contexts = new Dictionary<string, string[]>();
            //Contexts["SYS_REMEMBERED_SKILLS"] = new string[] { "1057" };
        }

        [JsonProperty("contexts")]
        public Dictionary<string, string[]> Contexts { get; set; }
    }
}