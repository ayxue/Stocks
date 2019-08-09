using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Dynamics.Models
{
    public class LeadModel
    {
        [JsonProperty("leadid")]
        public string LeadId { get; set; }

        [JsonProperty("emailaddress1")]
        public string Emailaddress1 { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("mobilephone")]
        public string Mobilephone { get; set; }

        [JsonProperty("telephone1")]
        public string Telephone1 { get; set; }

        public string CreatedLeadGuid { get; set; }
    }
}
