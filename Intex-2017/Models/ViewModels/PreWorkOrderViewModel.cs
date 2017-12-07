using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace Intex_2017.Models.ViewModels
{
    public class PreWorkOrderViewModel
    {
        public int LTNumber { get; set; }
        public String CompoundName { get; set; }
        public String SalesAgentName { get; set; }

        public bool BiochemicalPharmacology { get; set; }
        public bool DiscoveryScreen { get; set; }
        public bool ImmunoScreen { get; set; }
        public bool ProfilingScreen { get; set; }
        public bool PharmaScreen { get; set; }
        public bool CustomScreen { get; set; }
        public int CustID { get; set; }
        public decimal PriceQuote { get; set; }
    }
}