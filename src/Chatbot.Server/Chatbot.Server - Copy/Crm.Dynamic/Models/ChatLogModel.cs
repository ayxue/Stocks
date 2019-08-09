using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Crm.Dynamics.Models
{
    public class ChatLogModel
    {
        public static readonly Dictionary<string, string> ChatLogTags = new Dictionary<string, string>()
        {
            {"交易产品", "100000000" },
            {"交易平台", "100000001" },
            {"交易成本", "100000002" },
            {"开立账户", "100000003" },
            {"账户管理", "100000004" },
            {"入金和出金", "100000005" },
            {"转接人工", "100000006" },
            {"其他", "100000007" }
        };

        [JsonProperty("new_id")]
        public string SessionId { get; set; }

        [JsonProperty("new_tags")]
        public string Tags { get; set; }

        [JsonProperty("new_log")]
        public string Log { get; set; }

        [JsonProperty("new_LeadId@odata.bind")]
        public string LeadIdOdataBind { get; set; }

        public string CreatedChatLogGuid { get; set; }
    }
}
