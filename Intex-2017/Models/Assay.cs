using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("Assay")]
    public class Assay
    {
        [Key]
        public int AssayID { get; set; }

        public String AssayName { get; set; }
        public int WorkOrderID { get; set; }
        public String Protocol { get; set; }
        public int EstDuration { get; set; }
        public int StatusID { get; set; }
        public String QuantResults { get; set; }
        public String QualResutlts { get; set; }
        public decimal WorkHoursReq { get; set; }
        public DateTime StartDate { get; set; }
    }
}