using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class BillingInvoiceIndexViewModel
    {
        public int InvoiceID { get; set; }
        public int? WorkOrderID { get; set; }
        public String CompoundName { get; set; }
        public String CustFirstName { get; set; }
        public String CustLastName { get; set; }
        public String CustCompany { get; set; }
    }
}