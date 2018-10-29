using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Dynamic;
using Qin.Models;

namespace Trading.Data.Model.RefData
{
    [DebuggerDisplay("{ID} - {Symbol} - {Name}")]
    public class Instrument: DynamicModel
    {
        public int ID { get; set; }
        public string Symbol { get; set; }
        public string SymbolShort { get; set; }
        public string Name { get; set; }
        public int LotSize { get; set; }

    }
}
