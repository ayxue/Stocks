using HtmlAgilityPack;
using Qin.Html;
using Qin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Trading.Model.RefData;

namespace Trading.Data.Services.Sina.RefData
{
    public class EquityStructureService : BaseService
    {
        private static readonly string URL_TEMPLATE = "http://vip.stock.finance.sina.com.cn/corp/go.php/vCI_StockStructure/stockid/{0}.phtml";

        public EquityStructureService(HtmlService service) : base(service)
        {
        }

        public IEnumerable<EquityStructure> GetEquityStructure(string shortSymbol)
        {
            var url = string.Format(URL_TEMPLATE, shortSymbol);
            var doc = _service.GetHtml(url, HtmlService.GB2312);

            var ret = new List<EquityStructure>();
            var tables = doc.Y("table").Where(t => t.Id.StartsWith("StockStructureNewTable")).ToArray();
            for(int i = 0; i < tables.Length; i ++)
            {
                var rows = tables[i].Y("tbody tr");
                var tbl = rows.ToDataTableByColumn(true);
                foreach(DataRow row in tbl.Rows)
                {
                    var structure = new EquityStructure
                    {
                        Symbol = shortSymbol,
                        BeginTime = DateTime.Parse(row[tbl.GetColumn("变动日期")].ToString()),
                        Reason = row[tbl.GetColumn("变动原因")].ToString(),                      
                        TotalShareAmount = row[tbl.GetColumn("总股本")].ToString().Replace("万股", "").ToDecimal().Value,
                        PublicedAShare = row[tbl.GetColumn("流通A股")].ToString().Replace("万股", "").ToDecimal(),
                        LimitedAShare = row[tbl.GetColumn("限售A股")].ToString().Replace("万股", "").ToDecimal(),
                        HShare = row[tbl.GetColumn("流通H股")].ToString().Replace("万股", "").ToDecimal()
                    };
                    ret.Add(structure);
                }
            }

            return ret;
        }

    }
}
