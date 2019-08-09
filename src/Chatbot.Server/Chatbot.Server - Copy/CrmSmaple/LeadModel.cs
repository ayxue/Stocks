using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackConsoleApp
{
    public class LeadModel
    {
        public string emailaddress1 { get; set; }        
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string mobilephone { get; set; }
        public string telephone1 { get; set; }
    }

    public class RootObject
    {
        public List<LeadModel> value { get; set; }
    }
}
