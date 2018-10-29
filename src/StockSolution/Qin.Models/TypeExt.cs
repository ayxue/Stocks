using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Models
{
    public static class TypeExt
    {
        public static decimal? ToDecimal(this object obj)
        {
            var val = obj.ToString();
            return val.ToDecimal();
        }

        public static decimal? ToDecimal(this string val)
        {
            if (val == "--")
                return null;

            decimal amount;
            if (decimal.TryParse(val.Trim(), out amount))
                return amount;

            return null;
        }


    }
}
