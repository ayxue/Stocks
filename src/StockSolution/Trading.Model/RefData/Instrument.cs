using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Dynamic;
using Qin.Models;
using System.ComponentModel.DataAnnotations;

namespace Trading.Model.RefData
{
    [DebuggerDisplay("{ID} - {Symbol} - {Name}")]
    public class Instrument: DynamicModel
    {
        [Key]
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string SymbolShort { get; set; }

        public string Name { get; set; }

        public int LotSize { get; set; }

    }
}
