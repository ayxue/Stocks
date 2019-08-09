//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace Web.ApiSite.Controllers
//{
//    [RoutePrefix("api/fundamental")]
//    public class FundamentalController : ApiController
//    {
//        /// <summary>
//        /// 获得某个产品的股本结构历史
//        /// </summary>
//        /// <param name="shortSymbol"></param>
//        /// <returns></returns>
//        [HttpGet, Route("instrument/{shortSymbol}/equity")]
//        public EquityStructure[] GetEquityStructure(string shortSymbol)
//        {
//            return new EquityStructure[0];
//        }

//    }
//}