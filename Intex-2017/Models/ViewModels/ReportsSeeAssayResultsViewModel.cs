using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class ReportsSeeAssayResultsViewModel
    {
        public int LTNumber { get; set; }
        public int CompoundSeqCode { get; set; }
        public String QualResultsPath { get; set; }
        public String QuantResultsPath { get; set; }
    }
}