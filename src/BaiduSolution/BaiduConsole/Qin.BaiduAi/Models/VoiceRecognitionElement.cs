using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.BaiduAi.Models
{
    [DebuggerDisplay("{Text}")]
    public class VoiceRecognitionElement
    {
        private string[] _texts = null;

        [JsonProperty("sn")]
        public string Sn { get; set; }

        [JsonProperty("result")]
        public object Texts
        {
            get { return _texts; }
            set
            {
                var obj = value as JObject;
                if( obj != null && obj["word"] != null)
                    _texts = obj["word"].ToObject<string[]>();
                else
                {
                    var array = value as JArray;
                    if (array != null)
                        _texts = array.ToObject<string[]>();
                }
            }
        }

        public string Text
        {
            get { return _texts?.FirstOrDefault(); }
        }
    }
}
