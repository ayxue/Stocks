//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Results;
//using Newtonsoft.Json;
//using Web.ApiSite.Models;
//using Web.Models;

//namespace Web.ApiSite.Controllers
//{
//    /// <summary>
//    /// Reference Data
//    /// </summary>
//    [RoutePrefix("api/ref")]
//    public class ReferenceDataController : ApiController
//    {
//        [HttpGet, Route("app")]
//        public App GetApp()
//        {
//            return new App
//            {
//                Flow = "Code"
//            };
//        }



//        /// <summary>
//        /// Return a list of exchanges
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet, Route("exchanges")]
//        public Exchange[] GetExchanges()
//        {
//            var array = new Exchange[1];
//            array[0] = new Exchange
//            {
//                Name = "上交所"
//            };
//            return array;
//        }


//        /// <summary>
//        /// Returns a list of summary information for all instruments and options on the Saxo Trading platform restricted by the access rights of the user.
//        /// The resource serves as the starting point for an application/user who wants to navigate the available universe of instruments and options.Each entry therefore contains further references to instrument details or option roots, where the application can get further information about these entities.
//        /// </summary>
//        /// <param name="assetType">Comma separated list of one or more asset types to search for. E.g. AssetTypes=FxSpot,Stock</param>
//        /// <returns></returns>
//        [HttpGet, Route("instruments")]
//        public Instrument[] GetInstruments(string assetType = "")
//        {
//            var array = new Instrument[1];
//            array[0] = new Instrument
//            {
//                Identifier = 123,
//                Name = "asdad"
//            };
//            return array;
//        }

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


//        /// <summary>
//        /// 获得某个产品的分类
//        /// </summary>
//        /// <param name="shortSymbol"></param>
//        /// <returns></returns>
//        [HttpGet, Route("instrument/{shortSymbol}/category")]
//        public EquityStructure[] GetCategory(string shortSymbol)
//        {
//            return new EquityStructure[0];
//        }
//    }
//}
