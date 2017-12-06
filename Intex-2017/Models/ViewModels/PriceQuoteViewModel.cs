using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class PriceQuoteViewModel
    {
        public decimal PriceQuote { get; set; }

        public List<SampleTest> AssayCompoundList { get; set; }
    }
}