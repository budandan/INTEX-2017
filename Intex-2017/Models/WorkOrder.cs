using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("WorkOrder")]
    public class WorkOrder
    {
        [Key]
        public int WorkOrderID { get; set; }
        public String Comments { get; set; }
        public DateTime DateArrived { get; set; }

        [Required]
        public decimal ClientQuantity { get; set; }
        public int? ReceivedByWho { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDue { get; set; }

        [Required]
        public int CustID { get; set; }

        [Required]
        public int SalesAgentID { get; set; }
        public decimal ActualQuantity { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsVerified { get; set; }

        [Required]
        public int LTNumber { get; set; }
        public bool NeedsConditional { get; set; }
    }
}