using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qin.Html;
using Qin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Model.RefData;
using Trading.Data.Services.Sina.Model;
using Trading.Data.Services.Sina.RefData;

namespace Trading.Data.Tests.Services
{

    [TestClass]
    public class ETF50Tests
    {
        [TestMethod]
        public void _01_ETF50Tests()
        {
            var htmlService = new HtmlService();
            var instrumentService = new InstrumentService(htmlService);
            var etf50Insts = instrumentService.GetInstruments(SinaInstrumentCategory.Index.Index_Sh_50).Take(10);

            // Category
            var categService = new CategorizationService(htmlService);
            var categorizes = new HashSet<string>();
            foreach (var inst in etf50Insts)
            {
                var cates = categService.GetCateogories(inst.SymbolShort);
                foreach (var cate in cates)
                    categorizes.Add(cate);
                inst.AsDynamic().Categories = cates;
            }

            // Equity structure
            var structureService = new EquityStructureService(htmlService);
            foreach (var inst in etf50Insts)
            {
                var structure = structureService.GetEquityStructure(inst.SymbolShort);
                inst.AsDynamic().EquityStructure = structure;
            }

            var str = JsonUtil.Serialize(etf50Insts);
        }
    }
}
