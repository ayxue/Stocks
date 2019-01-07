using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace Trading.Model.Trade
{
    [DebuggerDisplay("{Symbol} - {Close} - {Time.ToString()}")]
    public class Price
    {
        [Key]
        public int RowId { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public decimal? Open { get; set; }

        public decimal? High { get; set; }

        public decimal? Close { get; set; }

        public decimal? Low { get; set; }

        public decimal? LastClose { get; set; }

        public decimal? Bid { get; set; }

        public decimal? Ask { get; set; }

        public decimal? Volumn { get; set; }

        public decimal? Amount { get; set; }
    }
}
