using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.ApiSite.Controllers
{
    /// <summary>
    /// Trading
    /// </summary>
    [RoutePrefix("api/trading")]
    public class TradingController : ApiController
    {
        /// <summary>
        /// 获取行情
        /// </summary>
        /// <param name="symbols">产品代码</param>
        /// <returns></returns>
        [HttpGet, Route("prices")]
        public Price[] GetPrices(string[] symbols)
        {
            return new Price[0];
        }

        /// <summary>
        /// 获取历史行情
        /// </summary>
        /// <param name="symbolShort">产品代码</param>
        /// <param name="begin">开始</param>
        /// <param name="end">结束</param>
        /// <returns></returns>
        [HttpGet, Route("prices/hist")]
        public Price[] GetPriceHistory(string symbolShort, DateTime? begin = null, DateTime? end = null)
        {
            return new Price[0];
        }


    }
}
