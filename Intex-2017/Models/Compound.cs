using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("Compound")]
    public class Compound
    {
        [Key]
        public int LTNumber { get; set; }
        public String CompoundName { get; set; }
        public String Appearance { get; set; }
        public decimal MolecularMass { get; set; }
        public decimal MTD { get; set; }
    }
}