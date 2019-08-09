using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HackConsoleApp
{
    class ChatLogModel
    {
        [JsonProperty("new_id")]
        public string SessionId { get; set; }

        [JsonProperty("new_tags")]
        public string Tags { get; set; }

        [JsonProperty("new_log")]
        public string Log { get; set; }

        [JsonProperty("new_LeadId@odata.bind")]
        public string LeadIdOdataBind { get; set; }
    }
}
