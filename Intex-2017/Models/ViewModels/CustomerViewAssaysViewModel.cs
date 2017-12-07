using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class CustomerViewAssaysViewModel
    {
        public String CompoundName { get; set; }
        public String AssayNameDesc { get; set; }
        public String StatusName { get; set; }
        public String DataReportPath { get; set; }
        public String SummaryReportPath { get; set; }
    }
}