using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Unit.Model
{
    /// <summary>
    /// Mapping to Baidu App: https://console.bce.baidu.com/ai/?fromai=1&locale=zh-cn#/ai/unit/app/list
    /// </summary>
    public class BaiduSession
    {
        public static BaiduSession CreateDefault(bool isInternal = false) => new BaiduSession
        {
            ClientId = "8bu383Ts9AXGmTGSsvSWM2Dz",
            ClientSecret = "HG1xPG1WkbiwUKw2Un8wXN7cfG25ikDK",
            IsInternal = isInternal
        };

        public bool IsInternal { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("session_key")]
        public string SesstionKey { get; set; }

        [JsonProperty("session_secret")]
        public string SesstionSecret { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
