using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.BaiduAi.Models
{
    [DebuggerDisplay("{Word}")]
    public class SentenceElement
    {
        public int ID { get; set; }

        public int ParentId { get; set; }

        public string Word { get; set; }

        public string Postag { get; set; }
    }
}
