using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
	[Table("PaymentMethod")]
	public class PaymentMethod
	{
		[Key]
		public int PaymentMethodID { get; set; }
		public String PaymentMethodName { get; set; }
	}
}