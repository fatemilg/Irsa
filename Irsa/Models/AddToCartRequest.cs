using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Models
{
    public class AddToCartRequest
    {
        public Guid PriceDetailID { get; set; }
        public string EnduserEmail { get; set; }
        public string EnduserCellPhone { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string DocID { get; set; }
        public string ExpireDate { get; set; }
        public string BirthCountry { get; set; }

        public string SeatRequest { get; set; }





    }
}
