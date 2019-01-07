using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trading.Model.RefData
{
    public class Exchange
    {
        [Key]
        public int Id { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }
    }
}
