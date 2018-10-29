using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qin.Html;
using Trading.Data.Model.RefData;
using Trading.Data.Model.Trade;

namespace Trading.Data.Services.Sina.Trade
{
    public class PriceHistoryService: BaseService
    {
        private static readonly string UrlTemplate_Daily = "http://vip.stock.finance.sina.com.cn/corp/go.php/vMS_MarketHistory/stockid/{0}.phtml";
        private static readonly string UrlTemplate_DailyParam = "?year={0}&jidu={1}";

        public PriceHistoryService(HtmlService service) : base(service)
        {
        }

        public IEnumerable<Price> GetDailyTadingInfo(string symbolShort, int year, int quarter)
        {
            var url = string.Format(UrlTemplate_Daily, symbolShort) + string.Format(UrlTemplate_DailyParam, year, quarter);
            var doc = _service.GetHtml(url, "GB2312");

            return ParseTradingInfo(doc, symbolShort);
        }

        public IEnumerable<Price> GetDailyTadingInfo(string symbolShort, DateTime? begin = null, DateTime? end = null)
        {
            if (begin > end)
                return Enumerable.Empty<Price>();

            var pairs = this.GetAvailablePeriods(symbolShort, end, begin);
            var ret = new List<Price>();

            var compareBegin = begin.HasValue ? begin : DateTime.MinValue;
            var compareEnd = end.HasValue ? end : DateTime.MaxValue;

            foreach(var pair in pairs)
            {
                var batch = GetDailyTadingInfo(symbolShort, pair.Item1, pair.Item2);
                foreach (var item in batch)
                    if (item.Time >= compareBegin && item.Time <= compareEnd)
                        ret.Add(item);
            }

            return ret.ToArray();
        }

        public IEnumerable<int> GetAvailableYears(string codeShort)
        {
            var url = string.Format(UrlTemplate_Daily, codeShort);
            var doc = _service.GetHtml(url, HtmlService.GB2312);
            var years = doc.Y("select[name='year'] option").Select(n => n.GetAttributeValue("value", 0)).ToArray();

            return years;
        }

        /// <summary>
        /// 返回可查交易信息的年与季度对
        /// </summary>
        public IEnumerable<Tuple<int, int>> GetAvailablePeriods(string codeShort, DateTime? endTime = null, DateTime? begin = null)
        {
            var years = this.GetAvailableYears(codeShort);

            if (endTime.HasValue && !begin.HasValue)
                begin = new DateTime(years.Min(), 1, 1);

            var start = begin.HasValue ? GetQuarterBegin(begin.Value) : new DateTime(DateTime.Today.Year, 1, 1);
            var end = endTime.HasValue ? GetQuarterEnd(endTime.Value) : GetQuarterEnd(DateTime.Today);

            if (start.Year > years.Max() || end.Year < years.Min())
                yield break;

            for (var date = end; date >= start; date = date.AddMonths(-3))
            {
                var quarter = GetQuarter(date);
                yield return new Tuple<int, int>(date.Year, quarter);
            }
        }

        private int GetQuarter(DateTime time)
        {
            return Convert.ToInt32(Math.Ceiling(time.Month / 3.0));
        }

        private DateTime GetQuarterEnd(DateTime time)
        {
            return new DateTime(time.Year, GetQuarter(time) * 3, 1).AddMonths(1).AddDays(-1);
        }

        private DateTime GetQuarterBegin(DateTime time)
        {
            return new DateTime(time.Year, 3 * (GetQuarter(time) - 1) + 1, 1);
        }

        private IEnumerable<Price> ParseTradingInfo(HtmlDocument doc, string symbol = "")
        {
            var rows = doc.Y("table#FundHoldSharesTable tr");
            foreach(var row in rows)
            {
                var tds = row.Y("td");
                if(tds.Any())
                {
                    var cells = tds.Text().ToArray();
                    DateTime date;
                    if( DateTime.TryParse(cells[0], out date))
                    {
                        yield return new Price
                        {
                            Time = date,
                            Symbol = symbol,
                            Open = Decimal.Parse(cells[1]),
                            High = Decimal.Parse(cells[2]),
                            Close = Decimal.Parse(cells[3]),
                            Low = Decimal.Parse(cells[4]),
                            Volumn = Decimal.Parse(cells[5]),
                            Amount = Decimal.Parse(cells[6])
                        };
                    }
                }
            }
        }

    }
}