using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("SummaryReport")]
    public class SummaryReport
    {
        [Key]
        public int SummaryReportID { get; set; }
        public String SummaryReportPath { get; set; }
        public int? WorkOrderID { get; set; }
    }
}