using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Trading.Model.RefData
{
    [DebuggerDisplay("{Symbol} - {TotalShareAmount} - {Time.ToString()}")]
    public class EquityStructure
    {
        public string Symbol { get; set; }

        public DateTime BeginTime { get; set; }

        public string Reason { get; set; }

        public decimal TotalShareAmount { get; set; }

        public decimal? PublicedAShare { get; set; }

        public decimal? LimitedAShare { get; set; }

        public decimal? HShare { get; set; }

    }
}
