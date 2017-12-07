using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("AssayName")]
    public class AssayName
    {
        [Key]
        public int AssayNameID { get; set; }
        public String AssayNameDesc { get; set; }
    }
}