using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        public int? WorkOrderID { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? TotalDue { get; set; }
        public bool? IsPaid { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TotalPaid { get; set; }
        public DateTime? EarlyDate { get; set; }
        public String InvoicePath { get; set; }
    }
}