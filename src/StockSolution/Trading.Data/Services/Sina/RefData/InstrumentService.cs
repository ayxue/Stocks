using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Qin.Html;
using Trading.Data.Model.RefData;
using Trading.Data.Services.Sina.Model;


namespace Trading.Data.Services.Sina.RefData
{
    public class InstrumentService: BaseService
    {
        private static readonly string GetInstrumentsTemplate = @"http://vip.stock.finance.sina.com.cn/quotes_service/api/json_v2.php/Market_Center.getHQNodeDataSimple?page={1}&num={2}&sort=symbol&asc=1&node={0}&_s_r_a=page";

        public InstrumentService(HtmlService service) : base(service)
        {
        }

        public Instrument[] GetInstruments(string instrumentCategory = "", int page = 1, int pageSize = 10000)
        {
            if (string.IsNullOrEmpty(instrumentCategory))
                instrumentCategory = SinaInstrumentCategory.ShZh_A;

            var url = string.Format(GetInstrumentsTemplate, instrumentCategory, page, pageSize);
            var rets = _service.Get(url);
            var items = JsonUtil.Deserialize(rets) as JArray;
            return items.Select(item => ParseInstrument(item)).ToArray();
        }

        private Instrument ParseInstrument(JToken item)
        {
            return new Instrument
            {
                Symbol = item.Value<string>("symbol"),
                SymbolShort = item.Value<string>("symbol").Substring(2),
                Name = item.Value<string>("name")
            };
        }
    }
}
