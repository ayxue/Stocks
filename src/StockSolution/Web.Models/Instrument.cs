using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Instrument
    {
        public int Identifier { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public string ExchangeId { get; set; }
    }
}
