using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.BaiduAi
{
    /// <summary>
    /// http://ai.baidu.com/docs#/NLP-Csharp-SDK/top
    /// </summary>
    public class BaiduApp
    {
        public BaiduApp()
        {
            Name = "Unicorn";
            AppId = "10528633";
            ApiKey = "RKut4UlvA1Mih5MQnI8kFitt";
            SecretKey = "THAAvphmv7tWUhYQpGumUYoLH7aufVNQ";
            CloudKey = "c29fc712499b43edafe72f4da27c7119";
            CloudSecret = "6617be2752954aae9f62df7b387325dd";
        }

        public string Name { get; set; }

        public string AppId { get; set; }

        public string ApiKey { get; set; }

        public string SecretKey { get; set; }

        public string CloudKey { get; set; }

        public string CloudSecret { get; set; }
    }
}
