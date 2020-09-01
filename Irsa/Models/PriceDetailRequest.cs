using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Models
{
    public class PriceDetailRequest
    {
        public Guid SearchID { get; set; }
        public string SelectedFlightIDList { get; set; }
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
        public string InfantCount { get; set; }
        public string Currency { get; set; }
        public string LanguageCode { get; set; }
        public string FareFamilyID { get; set; }
    }
}
