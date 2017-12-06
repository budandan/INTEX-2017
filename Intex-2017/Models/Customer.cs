using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		    [DisplayName("Customer ID")]
        public int CustID { get; set; }

		    [Required]
		    [DisplayName("First Name")]
		    public String CustFirstName { get; set; }

		    [Required]
	    	[DisplayName("Last Name")]
		    public String CustLastName { get; set; }

		    [Required]
	    	[DisplayName("Address 1")]
	    	public String CustAddress1 { get; set; }

	    	[DisplayName("Address 2")]
	    	public String CustAddress2 { get; set; }

	    	[Required]
	    	[DisplayName("City")]
	    	public String CustCity { get; set; }

	    	[Required]
	    	[StringLength(2, MinimumLength = 2)]
	    	[DisplayName("State")]
	    	public String CustState { get; set; }

		    [Required]
	    	[DataType(DataType.PostalCode)]
	    	[DisplayName("Zip")]
	    	public String CustZip { get; set; }

	    	[Required]
    		[DataType(DataType.EmailAddress)]
	    	[DisplayName("Email Address")]
    		public String CustEmail { get; set; }


	    	[DataType(DataType.PhoneNumber)]
	    	[DisplayName("Phone Number")]
	    	public String CustPhone { get; set; }

    		[Required]
    		[DisplayName("Payment Method")]
	    	public int PaymentMethodID { get; set; }

	    	[Required]
	    	[StringLength(20, MinimumLength = 5)]
	    	[DisplayName("User Name")]
	    	public String CustUserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public String CustPassword { get; set; }
    }
}