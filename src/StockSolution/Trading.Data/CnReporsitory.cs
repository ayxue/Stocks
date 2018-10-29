//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net;
//using Trading.Data.Services.Sina;
//using Newtonsoft.Json;
//using Trading.Data.Services;
//using System.IO;
//using System.Threading;
//using Trading.Data.Services.Sina.Trade;
//using Trading.Data.Model.Trade;
//using Trading.Data.Model.RefData;

//namespace Trading.Data
//{
//    public class CnReporsitory
//    {
//        #region Static
//        private static readonly int PageSize = 80;
//        private static readonly string FilePath = Path.Combine("reporsitory", "list.txt");
//        #endregion

//        private InfoPriceService _listService;
//        private HistoricalInfoPriceService _tradeService;

//        public CnReporsitory(InfoPriceService listService, HistoricalInfoPriceService tradeService)
//        {
//            this._listService = listService;
//            this._tradeService = tradeService;
//        }

//        public IEnumerable<Instrument> GetProductList(bool fromWebIfNoLocal = false)
//        {
//            var codes = GetListFromFile();

//            if (fromWebIfNoLocal && !codes.Any())
//                codes = GetListFromWeb();

//            return codes;
//        }

//        public IEnumerable<InfoPrice> GetDailyTadingInfo(string codeShort, DateTime? end = null, DateTime? begin = null)
//        {
//            var periods = _tradeService.GetAvailablePeriods(codeShort, end, begin).ToArray();
//            return periods.SelectMany(period =>
//            {
//                return _tradeService.GetDailyTadingInfo(codeShort, period.Item1, period.Item2).ToArray();
//            });
//        }


//        #region Pivate

//        /// <summary>
//        /// Get product list from file
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<Instrument> GetListFromFile()
//        {
//            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);
//            var content = File.ReadAllText(filePath);

//            if (string.IsNullOrEmpty(content))
//                return Enumerable.Empty<Instrument>();

//            return JsonUtil.Deserialize<Instrument[]>(content);

//        }

//        /// <summary>
//        /// Get product list from web
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<Instrument> GetListFromWeb()
//        {
//            var result = _listService.GetResult(1);
//            return null;
//            //decimal page = Math.Ceiling(Convert.ToDecimal(result.Count) / PageSize);

//            //for(int i = 1; i <= page; i ++)
//            //{
//            //    var pageResult = _listService.GetResult(PageSize, i);
//            //    foreach(var item in pageResult.Items)
//            //    {
//            //        yield return new Instrument {
//            //            Symbol = item[0],
//            //            SymbolShort = item[1],
//            //            Name = item[2]
//            //        };
//            //    }
//            //}
//        }
//        #endregion

//    }
//}
