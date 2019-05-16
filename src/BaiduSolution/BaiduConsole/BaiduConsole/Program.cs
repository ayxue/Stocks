using Qin.Audio;
using Qin.BaiduAi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiduConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new BaiduApp();
            var faceApp = new BaiduApp
            {
                AppId = "15460157",
                ApiKey = "m8hZWttdHmeaH2sE92sfu7Om",
                SecretKey = "CBftkOccAaUQfD9YrollILxvrRLdQUxW",
                CloudKey = "",
                CloudSecret = ""
            };

            var client = new BaiduClient(app, faceApp);

            //var testText = "据报道，12月26日，国务委员兼外交部长王毅出席全国地方外办主任会议并宣布2019年外交部支持地方外事工作的“六件实事”。你能否介绍“六件实事”的具体内容？";
            //var ret = client.SentenceParse(testText);

            //var voiceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testEn.wav");
            //var retEn = client.VoiceToText(voiceFile, true);

            var voiceFileCn = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test2.wav");
            var retCn = client.VoiceToText(voiceFileCn);

            //var iamge1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trump.jpg");
            //var benchmark = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trump2.jpg");
            //var ret = client.CompareFace(iamge1, benchmark);

            // Speak
            //var path = client.Speak("啊哦额亦五与");

            //var devices = WinRecorder.Devices;
        }
    }
}
