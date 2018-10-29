using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Data.Services.Model.Sina
{
    public class SinaResult
    {
        public string Code { get; set; }
        public string Day { get; set; }
        public int Count { get; set; }
        public string[] Fields { get; set; }
        public dynamic[] Items { get; set; }

    }
}
