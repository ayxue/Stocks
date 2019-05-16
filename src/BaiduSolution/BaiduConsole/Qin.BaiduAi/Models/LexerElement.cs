using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.BaiduAi.Models
{
    [DebuggerDisplay("{Word} - {PosType}")]
    public class LexerElement
    {
        [JsonProperty("basic_words")]
        public string[] BasicWords { get; set; }

        public string Word
        {
            get
            {
                return BasicWords.FirstOrDefault();
            }  
        }

        [JsonProperty("pos")]
        public string PosType { get; set; }
    }
}
