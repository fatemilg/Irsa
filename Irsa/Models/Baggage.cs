using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Models
{
    public class Baggage
    {
        [Key]
        public int Index { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
    }
}
