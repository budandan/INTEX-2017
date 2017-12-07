using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class LabTechScheduleOneAssayViewModel
    {
        public int AssayID { get; set; }
        public int? WorkOrderID { get; set; }
        public String CustCompany { get; set; }
        public String CustFirstName { get; set; }
        public String CustLastName { get; set; }
        public String AssayNameDesc { get; set; }
        public String CompoundName { get; set; }
        public DateTime DueDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public String Comments { get; set; }
    }
}