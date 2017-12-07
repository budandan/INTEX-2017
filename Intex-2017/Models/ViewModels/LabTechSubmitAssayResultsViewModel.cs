using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class LabTechSubmitAssayResultsViewModel
    {
        public int AssayID { get; set; }
        public int LTNumber { get; set; }
        public String CompoundName { get; set; }
        public DateTime DueDate { get; set; }
    }
}