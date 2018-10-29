using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Trading.Data.Model.RefData;

namespace Web.ApiSite.Controllers
{
    /// <summary>
    /// Reference Data
    /// </summary>
    [RoutePrefix("api/ref")]
    public class ReferenceDataController : ApiController
    {
        /// <summary>
        /// 获得所有产品
        /// </summary>
        /// <param name="category">产品分类</param>
        /// <param name="page">第几页</param>
        /// <param name="pageSize">每页多少条记录</param>
        /// <returns></returns>
        [HttpGet, Route("instruments")]
        public Instrument[] GetInstruments(string category = "", int page = 1, int pageSize = 10000)
        {
            var array = new Instrument[1];
            array[0] = new Instrument
            {
                ID = 123,
                Name = "asdad"
            };
            return array;
        }

        /// <summary>
        /// 获得某个产品的股本结构历史
        /// </summary>
        /// <param name="shortSymbol"></param>
        /// <returns></returns>
        [HttpGet, Route("instrument/{shortSymbol}/equity")]
        public EquityStructure[] GetEquityStructure(string shortSymbol)
        {
            return new EquityStructure[0];
        }


        /// <summary>
        /// 获得某个产品的分类
        /// </summary>
        /// <param name="shortSymbol"></param>
        /// <returns></returns>
        [HttpGet, Route("instrument/{shortSymbol}/category")]
        public EquityStructure[] GetCategory(string shortSymbol)
        {
            return new EquityStructure[0];
        }
    }
}
