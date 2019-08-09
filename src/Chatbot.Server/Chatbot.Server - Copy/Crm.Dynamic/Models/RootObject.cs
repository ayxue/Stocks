using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Dynamics.Models
{
    public class RootObject<T>
    {
        [JsonProperty("value")]
        public T[] Value { get; set; }
    }
}
