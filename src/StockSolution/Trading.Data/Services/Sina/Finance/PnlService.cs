using Qin.Html;
using Qin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Model.Finance;

namespace Trading.Data.Services.Sina.Finance
{
    public class PnlService: BaseService
    {
        private static readonly string YEARS_TEMPLATE = "http://money.finance.sina.com.cn/corp/go.php/vFD_ProfitStatement/stockid/{0}/ctrl/part/displaytype/4.phtml";
        private static readonly string URL_TEMPLATE = "http://money.finance.sina.com.cn/corp/go.php/vFD_ProfitStatement/stockid/{0}/ctrl/{1}/displaytype/4.phtml";

        public PnlService(HtmlService service): base(service)
        {
        }

        public int[] GetYears(string shortSymbol)
        {
            var url = string.Format(YEARS_TEMPLATE, shortSymbol);
            var doc = _service.GetHtml(url, HtmlService.GB2312);
            var links = doc.Y("div#con02-1 table").First().Y("tr td a").ToArray();

            return links.Select(l => int.Parse(l.InnerText)).ToArray();
        }


        public List<PnlTable> GetPnlTable(string shortSymbol, int year)
        {
            var url = string.Format(URL_TEMPLATE, shortSymbol, year);
            var doc = _service.GetHtml(url, HtmlService.GB2312);

            var ret = new List<PnlTable>();
            var rows = doc.Y("table#ProfitStatementNewTable0 tbody tr").ToArray();
            var tbl = rows.ToDataTableByColumn(true);
            foreach (DataRow row in tbl.Rows)
            {
                var pnl = new PnlTable
                {
                    Symbol = shortSymbol,
                    Time = DateTime.Parse(row[tbl.GetColumn("报表日期")].ToString()),
                    Income = row[tbl.GetColumn("一、营业收入")].ToDecimal(),
                    Expense = row[tbl.GetColumn("二、营业支出")].ToDecimal(),
                    OperationalProfit = row[tbl.GetColumn("三、营业利润")].ToDecimal(),
                    TotalProfit = row[tbl.GetColumn("四、利润总额")].ToDecimal(),
                    NetProfit = row[tbl.GetColumn("五、净利润")].ToDecimal()
                };
                ret.Add(pnl);
            }

            return ret;
        }

    }
}
