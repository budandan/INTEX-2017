using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("DataReport")]
    public class DataReport
    {
        [Key]
        public int DataReportID { get; set; }
        public String DataReportPath { get; set; }
        public int? AssayID { get; set; }
    }
}