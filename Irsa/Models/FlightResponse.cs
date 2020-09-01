using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Models
{
    public class FlightResponse
    {
        public Guid SearchID { get; set; }
        public FlightItems FlightItems { get; set; }

    }
}
