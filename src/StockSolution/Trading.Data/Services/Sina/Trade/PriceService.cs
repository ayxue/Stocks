using Newtonsoft.Json;
using Qin.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Model.Trade;

namespace Trading.Data.Services.Sina.Trade
{
    /// <summary>
    /// http://finance.sina.com.cn/data/#stock
    /// sends a request to an open api to get the results
    /// </summary>
    public class PriceService: BaseService
    {
        //private static readonly string TemplateListAddress = "http://money.finance.sina.com.cn/d/api/openapi_proxy.php/?__s=[[%22hq%22,%22hs_a%22,%22%22,0,{0},{1}]]";
        private static readonly string TemplateListAddress = "http://hq.sinajs.cn/rn=rwify&list=";
        private static readonly string SymbolBegin = "var hq_str_";
        private static readonly string ValueBegin = "\"";

        public PriceService(HtmlService service) : base(service)
        {
        }

        public IEnumerable<Price> GetInfoPrices(params string[] symbols)
        {
            var address = string.Concat(TemplateListAddress, string.Join(",", symbols));
            var content = _service.Get(address);
            foreach(var line in content.Split('\n'))
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                var valueBeginIndex = line.IndexOf(ValueBegin);
                var symbol = line.Substring(SymbolBegin.Length, valueBeginIndex - SymbolBegin.Length - 1);
                var values = line.Substring(valueBeginIndex + 1, line.Length - valueBeginIndex - 3).Split(',');
                yield return new Price
                {
                    Symbol = symbol,
                    Name = values[0],
                    Time = DateTime.Parse(values[values.Length - 3] + " " + values[values.Length - 2] + "." + values[values.Length - 1]),
                    Open = decimal.Parse(values[1]),
                    High = decimal.Parse(values[4]),
                    Close = decimal.Parse(values[3]),
                    Low = decimal.Parse(values[5]),
                    LastClose = decimal.Parse(values[2]),
                    Bid = decimal.Parse(values[6]),
                    Ask = decimal.Parse(values[7]),
                    Volumn = decimal.Parse(values[8]), // 成交量，手
                    Amount = decimal.Parse(values[9])  // 成交额, 万
                };
            }
        }

        public string GetServiceAddress()
        {
            return TemplateListAddress + "sh600030";
        }
    }
}
