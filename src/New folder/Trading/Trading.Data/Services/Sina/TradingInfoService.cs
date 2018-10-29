using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qin.Html;
using Trading.Data.Services.Sina.Model;

namespace Trading.Data.Services.Sina
{
    public class TradingInfoService
    {
        private static readonly string UrlTemplate_Daily = "http://vip.stock.finance.sina.com.cn/corp/go.php/vMS_MarketHistory/stockid/{0}.phtml";
        private static readonly string UrlTemplate_DailyParam = "?year={0}&jidu={1}";

        public IEnumerable<TradingInfo> GetDailyTadingInfo(string codeShort, int year, int quarter)
        {
            var url = string.Format(UrlTemplate_Daily, codeShort) + string.Format(UrlTemplate_DailyParam, year, quarter);
            var http = new HttpService();
            var doc = http.GetHtml(url);

            return ParseTradingInfo(doc);
        }

        public IEnumerable<int> GetAvailableYears(string codeShort)
        {
            var url = string.Format(UrlTemplate_Daily, codeShort);
            var http = new HttpService();
            var doc = http.GetHtml(url);
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

        private IEnumerable<TradingInfo> ParseTradingInfo(HtmlDocument doc)
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
                        yield return new TradingInfo
                        {
                            Time = date,
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