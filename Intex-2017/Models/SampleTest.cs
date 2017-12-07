using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("SampleTest")]
    public class SampleTest
    {
        [Key]
        public int SampleTestID { get; set; }
        public int LTNumber { get; set; }
        public int AssayID { get; set; }
        public int CompoundSeqCode { get; set; }
        public int TestTubeID { get; set; }
        public bool IsRequired { get; set; }
        public decimal Cost { get; set; }
        public String QualResultsPath { get; set; }
        public String QuantResultsPath { get; set; }
    }
}