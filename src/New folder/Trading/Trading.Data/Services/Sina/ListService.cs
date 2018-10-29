using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Services.Model.Sina;

namespace Trading.Data.Services.Sina
{
    /// <summary>
    /// http://finance.sina.com.cn/data/#stock
    /// sends a request to an open api to get the results
    /// </summary>
    public class ListService
    {
        private static readonly string TemplateListAddress = "http://money.finance.sina.com.cn/d/api/openapi_proxy.php/?__s=[[%22hq%22,%22hs_a%22,%22%22,0,{0},{1}]]&callback=FDC_DC.theTableData";
        private static readonly string Header = "/*<script>location.href='//sina.com';</script>*/\nFDC_DC.theTableData([";
        private static readonly string Tail = "])";

        public SinaResult GetResult(int size = 40, int page = 1)
        {
            var http = new HttpService();
            var address = this.GetServiceAddress(size, page);
            var content = http.Get(address);
            var json = this.ResultToJson(content);
            return JsonConvert.DeserializeObject<SinaResult>(json);
        }

        public string GetServiceAddress(int size = 40, int page = 1)
        {
            if (page < 1)
                page = 1;

            return string.Format(TemplateListAddress, page, size);
        }

        public string ResultToJson(string content)
        {
            var jsonLength = content.Length - Header.Length - Tail.Length;
            return content.Substring(Header.Length, jsonLength - 1);
        }
    }
}
