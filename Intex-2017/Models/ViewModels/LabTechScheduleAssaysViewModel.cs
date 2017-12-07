using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class LabTechScheduleAssaysViewModel
    {
        public int? WorkOrderID { get; set; }
        public int AssayID { get; set; }
        public String AssayNameDesc { get; set; }
        public String CompoundName { get; set; }
        public decimal ClientQuantity { get; set; }
    }
}