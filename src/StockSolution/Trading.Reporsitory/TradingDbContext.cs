using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Model.RefData;
using Trading.Model.Trade;

namespace Trading.Reporsitory
{
    public class TradingDbContext: DbContext
    {
        public TradingDbContext(): base("TradingDbConnection")
        {
        }

        public TradingDbContext(string connName) : base(connName)
        {
        }

        public DbSet<Exchange> Exchange { get; set; }

        public DbSet<Instrument> Instruments { get; set; }

        public DbSet<Price> Prices { get; set; }
    }
}
