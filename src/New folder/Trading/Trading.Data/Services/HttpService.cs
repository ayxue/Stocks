using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trading.Data.Services
{
    public class HttpService
    {
        public static string DefaultEncoding = "GB2312";

        public HtmlDocument GetHtml(string address)
        {
            return GetHtml(address, DefaultEncoding);
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
            return this.Get(address, DefaultEncoding);
        }

        public string Get(string address, string encoding)
        {
            var data = this.GetRaw(address);
            return Encoding.GetEncoding(encoding).GetString(data);
        }

        public byte[] GetRaw(string address)
        {
            Thread.Sleep(400);
            using (var client = new WebClient())
            {
                return client.DownloadData(address);
            }
        }

    }
}
