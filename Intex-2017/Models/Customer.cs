using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustID { get; set; }
        public String CustFirstName { get; set; }
        public String CustLastName { get; set; }
        public String CustAddress1 { get; set; }
        public String CustAddress2 { get; set; }
        public String CustCity { get; set; }
        public String CustState { get; set; }
        public String CustZip { get; set; }
        public String CustEmail { get; set; }
        public String CustPhone { get; set; }
        public int PaymentMethodID { get; set; }
        public String CustPassword { get; set; }
    }
}