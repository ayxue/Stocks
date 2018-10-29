using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Trading.Data.Model.Finance;
using Trading.Data.Model.RefData;

namespace Web.ApiSite.Controllers
{
    /// <summary>
    /// Finance Data
    /// </summary>
    [RoutePrefix("api/finance")]
    public class FinanceController : ApiController
    {
        /// <summary>
        /// 获取利润表
        /// </summary>
        /// <param name="shortSymbol"></param>
        /// <returns></returns>
        [HttpGet, Route("pnl")]
        public PnlTable[] GetPnlTables(string shortSymbol)
        {
            return new PnlTable[0];
        }
    }
}
