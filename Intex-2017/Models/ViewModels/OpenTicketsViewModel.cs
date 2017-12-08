using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class OpenTicketsViewModel
    {
        public int CallTicketID { get; set; }
        public String CustFirstName { get; set; }
        public String CustLastName { get; set; }
        public String CustCompany { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public String CustPhone { get; set; }
    }
}