using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Models
{
    public class FareRuleRequest
    {
        public Guid SearchID { get; set; }
        public string SelectedFlightIDList { get; set; }
    }
}
