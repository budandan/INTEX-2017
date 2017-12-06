using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class PreWorkOrderViewModel
    {
        public int LTNumber { get; set; }
        public String CompoundName { get; set; }
        public List<int> AssayIDlist { get; set; }
        public decimal PriceQuote { get; set; }
    }
}