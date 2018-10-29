using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qin.Html
{
    public class HtmlService
    {
        private static readonly int SleepTime = 400;

        public static string GB2312 = "GB2312";
        public static string UTF8 = "UTF8";

        static HtmlService()
        {
            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public HtmlDocument GetHtml(string address)
        {
            return GetHtml(address, UTF8);
        }

        public HtmlDocument GetHtml(string address, string encoding)
        {
            var content = this.Get(address, encoding);
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            return doc;
        }

        public string Get(string address)
        {
            return Get(address, GB2312);
        }

        public string Get(string address, string encoding)
        {
            var data = this.GetRaw(address);
            return Encoding.GetEncoding(encoding).GetString(data);
        }

        public byte[] GetRaw(string address)
        {
            Thread.Sleep(SleepTime);
            using (var client = new WebClient())
            {
                return client.DownloadData(address);
            }
        }
    }
}
