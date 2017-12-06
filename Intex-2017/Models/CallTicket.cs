using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("CallTicket")]
    public class CallTicket
    {
        [Key]
        public int CallTicketID { get; set; }

        public int CustID { get; set; }

        public String Subject { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public String Body { get; set; }

        public bool IsOpen { get; set; }
    }
}