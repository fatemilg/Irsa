using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Irsa.Models
{
    public class SeatRequest
    {
  
        public Guid SearchID { get; set; }
        public string SelectedFlightIDList { get; set; }

    }

    public class SeatRequestInAddToCart
    {

        public string RowNumber { get; set; }
        public string ColumnName { get; set; }
        public string ItineraryReference { get; set; }
        public string SegmentReference { get; set; }

    }
}
