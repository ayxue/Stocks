using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PnlTable
    {
        public string Symbol { get; set; }

        public DateTime Time { get; set; }

        public decimal? Income { get; set; }

        public decimal? Expense { get; set; }

        public decimal? OperationalProfit { get; set; }

        public decimal? TotalProfit { get; set; }

        public decimal? NetProfit { get; set; }
    }
}
