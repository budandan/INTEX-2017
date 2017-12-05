using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Intex_2017.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [DisplayName("Employee ID")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [RegularExpression("^[a-zA-Z''-'\\s]{1,40}$", ErrorMessage = "First name must be between 1 and 40 characters, contain only A-Z characters or \' or - symbols")]
        [DisplayName("First Name")]
        public String EmpFirstName { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [RegularExpression("^[a-zA-Z''-'\\s]{1,40}$", ErrorMessage = "Last name must be between 1 and 40 characters, contain only A-Z characters or \' or - symbols")]
        [DisplayName("Last Name")]
        public String EmpLastName { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [DisplayName("Address Line 1")]
        public String EmpAddress1 { get; set; }

        [DisplayName("Address Line 2")]
        public String EmpAddress2 { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [DisplayName("City")]
        public String EmpCity { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [DisplayName("State")]
        public String EmpState { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [RegularExpression("^(\\d{5}-\\d{4}|\\d{5}|\\d{9})$|^([a-zA-Z]\\d[a-zA-Z] \\d[a-zA-Z]\\d)$", ErrorMessage = "Please enter a valid 5 digit zip code")]
        [DisplayName("Zip")]
        public String EmpZip { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        [DisplayName("Email")]
        public String EmpEmail { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [RegularExpression("^[01]?[- .]?(\\([2-9]\\d{2}\\)|[2-9]\\d{2})[- .]?\\d{3}[- .]?\\d{4}$", ErrorMessage = "Please enter a valid 10-digit phone number in the 555-555-5555 format")]
        [DisplayName("Phone Number")]
        public String EmpPhone { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [DisplayName("Password")]
        public String EmpPassword { get; set; }

        [DisplayName("Role")]
        public String Role { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [DisplayName("Hourly Wage")]
        public decimal Wage { get; set; }

        [Required(ErrorMessage = "This field cannot be blank")]
        [DisplayName("Username")]
        public String EmpUsername { get; set; }
    }
}