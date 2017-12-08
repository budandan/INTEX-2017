using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class ManagerDataReportsViewModel
    {
        public int WorkOrderID { get; set; }
        public int AssayID { get; set; }
        public int LTNumber { get; set; }
        public String CompoundName { get; set; }
        public String CustFirstName { get; set; }
        public String CustLastName { get; set; }
        public String CustCompany { get; set; }
        public String DataReportPath { get; set; }
    }
}