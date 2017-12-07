using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class CustomerSeeWorkOrdersViewModel
    {
        public int WorkOrderID { get; set; }
        public DateTime DateDue { get; set; }
        public String CompoundName { get; set; }
        public bool IsVerified { get; set; }
    }
}