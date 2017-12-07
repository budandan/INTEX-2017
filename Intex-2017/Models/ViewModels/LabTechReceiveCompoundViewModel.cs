using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class LabTechReceiveCompoundViewModel
    {
        public int WorkOrderID { get; set; }
        public String CustFirstName { get; set; }
        public String CustLastName { get; set; }
        public String CompoundName { get; set; }
        public decimal ClientQuantity { get; set; }
        public decimal ActualQuantity { get; set; }
    }
}