using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class LabTechSubmitSampleResultsViewModel
    {
        public int SampleTestID { get; set; }
        public int AssayID { get; set; }
        public String TestTubeNumber { get; set; }
        public String CompoundName { get; set; }
        public bool HasQuant { get; set; }
        public bool HasQual { get; set; }
    }
}