using Baidu.Aip.Face;
using Baidu.Aip.Nlp;
using Baidu.Aip.Speech;
using Newtonsoft.Json.Linq;
using Qin.BaiduAi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.BaiduAi
{
    public class BaiduClient   
    {
        public BaiduApp App { get; private set; }

        public BaiduApp FaceApp { get; private set; }

        public int Timeout { get; set; }

        public BaiduClient(BaiduApp app, BaiduApp faceApp)
        {
            this.App = app;
            Timeout = 60000;

            this.FaceApp = faceApp;
        }

        private Nlp CreateLanguageClient()
        {
            var apiKey = string.IsNullOrEmpty(App.CloudKey) ? App.ApiKey : App.CloudKey;
            var secretKey = string.IsNullOrEmpty(App.CloudSecret) ? App.SecretKey : App.CloudSecret;
            var client = new Nlp(apiKey, secretKey);
            client.Timeout = 60000;  
            return client;
        }

        private Asr CreateSpeechClient()
        {
            var client = new Asr(App.AppId, App.ApiKey, App.SecretKey);
            client.Timeout = 120000;
            return client;
        }

        private Tts CreateVoiceClient()
        {
            var client = new Tts(App.ApiKey, App.SecretKey);
            client.Timeout = 120000;
            return client;
        }

        private Face CreateFaceClient()
        {
            var client = new Face(FaceApp.ApiKey, FaceApp.SecretKey);
            client.Timeout = 60000;
            return client;
        }

        public BaiduApiResponse<LexerElement[]> LexerParse(string text)
        {
            var client = CreateLanguageClient();
            var ret = client.Lexer(text);

            return ret.ToResponse<LexerElement[]>("items", true);
        }

        /// <summary>
        /// 句法分析
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaiduApiResponse<SentenceElement[]> SentenceParse(string text)
        {
            var client = CreateLanguageClient();
            var ret = client.DepParser(text);

            return ret.ToResponse<SentenceElement[]>("items", true);
        }

        /// <summary>
        /// 语音识别
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isEn"></param>
        /// <returns></returns>
        public BaiduApiResponse<VoiceRecognitionElement> VoiceToText(string path, bool isEn = false)
        {
            var client = CreateSpeechClient();

            var pid = isEn ? 1737 : 1536;
            using (var stream = File.OpenRead(path))
            {
                var result = client.Recognize(stream, App.AppId, "wav", 16000, pid);
                return result.ToResponse<VoiceRecognitionElement>("", true);
            }
        }

        public string Speak(string text)
        {
            var client = CreateVoiceClient();
            var option = new Dictionary<string, object>()
            {
                {"spd", 3}, // 语速
                {"vol", 7}, // 音量
                {"per", 4}  // 发音人，4：情感度丫丫童声
            };
            var result = client.Synthesis(text, option);

            if (!result.Success)
                return result.ErrorMsg;
            
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "111.mp3");
            File.WriteAllBytes(path, result.Data);
            return path;
        }
            

        public BaiduApiResponse<decimal> CompareFace(string imagePath, string benchmarkPath)
        {
            var client = CreateFaceClient();
            var faces = new JArray
            {
                new JObject
                {
                    {"image", ReadImg(imagePath)},
                    {"image_type", "BASE64"},
                    {"face_type", "LIVE"},
                    {"quality_control", "LOW"},
                    {"liveness_control", "NONE"},
                },
                new JObject
                {
                    {"image", ReadImg(benchmarkPath)},
                    {"image_type", "BASE64"},
                    {"face_type", "LIVE"},
                    {"quality_control", "LOW"},
                    {"liveness_control", "NONE"},
                }
            };

            var result = client.Match(faces);
            return result.ToResponse<decimal>("result.score", true); ;
        }

        public string ReadImg(string img)
        {
            return Convert.ToBase64String(File.ReadAllBytes(img));
        }

    }
}
