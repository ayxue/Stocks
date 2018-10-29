using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Trading.Data.Services.Sina.Model
{
    [DebuggerDisplay("{Time.ToString()} - {Close}/{AveragePrice}, {Volumn}")]
    public class TradingInfo
    {
        public DateTime Time { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Close { get; set; }

        public decimal Low { get; set; }

        public decimal Volumn { get; set; }

        public decimal Amount { get; set; }

        public decimal AveragePrice
        {
            get
            {
                return Math.Round(Amount / Volumn, 2);
            }
        }

    }
}
