using Qin.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Services.Sina;

namespace Trading.Data.Tests.Services
{
    public class CategorizationService: BaseService
    {
        private static readonly string URL_TEMPLATE = "http://vip.stock.finance.sina.com.cn/corp/go.php/vCI_CorpOtherInfo/stockid/{0}/menu_num/5.phtml";

        public CategorizationService(HtmlService service): base(service)
        {
        }

        public string[] GetCateogories(string shortSymbol)
        {
            var url = string.Format(URL_TEMPLATE, shortSymbol);
            var doc = _service.GetHtml(url, HtmlService.GB2312);

            var rows = doc.Y("div#con02-0 table tr");
            var categories = new List<string>();
            foreach (var row in rows)
            {
                var tds = row.Y("td").ToArray();
                if(tds.Length > 1)                
                    categories.Add(tds[0].Text());
            }

            return categories.ToArray();
        }
    }
}
