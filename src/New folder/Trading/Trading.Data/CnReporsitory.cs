using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Trading.Data.Services.Sina;
using Newtonsoft.Json;
using Trading.Data.Services;
using System.IO;
using System.Threading;
using Trading.Data.Services.Sina.Model;

namespace Trading.Data
{
    public class CnReporsitory
    {
        #region Static
        private static readonly int PageSize = 80;
        private static readonly string FilePath = Path.Combine("reporsitory", "list.txt");      
        #endregion

        private ListService _listService = new ListService();
        private TradingInfoService _tradeService = new TradingInfoService();

        public IEnumerable<ProductCode> GetProductList(bool fromWebIfNoLocal = false)
        {
            var codes = GetListFromFile();

            if (fromWebIfNoLocal && !codes.Any())
                codes = GetListFromWeb();

            return codes;
        }


        public IEnumerable<TradingInfo> GetDailyTadingInfo(string codeShort, DateTime? end = null, DateTime? begin = null)
        {
            var periods = _tradeService.GetAvailablePeriods(codeShort, end, begin).ToArray();
            return periods.SelectMany(period =>
            {
                return _tradeService.GetDailyTadingInfo(codeShort, period.Item1, period.Item2).ToArray();
            });
        }


        #region Pivate

        /// <summary>
        /// Get product list from file
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductCode> GetListFromFile()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);
            var content = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(content))
                return Enumerable.Empty<ProductCode>();

            return JsonUtil.Deserialize<ProductCode[]>(content);

        }

        /// <summary>
        /// Get product list from web
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductCode> GetListFromWeb()
        {
            var result = _listService.GetResult(1);
            decimal page = Math.Ceiling(Convert.ToDecimal(result.Count) / PageSize);

            for(int i = 1; i <= page; i ++)
            {
                var pageResult = _listService.GetResult(PageSize, i);
                foreach(var item in pageResult.Items)
                {
                    yield return new ProductCode {
                        Code = item[0],
                        CodeShort = item[1],
                        Name = item[2]
                    };
                }
            }
        }
        #endregion

    }
}
