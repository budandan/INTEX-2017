using System;
using System.Collections.Generic;
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
        public int EmployeeID { get; set; }
        public String EmpFirstName { get; set; }
        public String EmpLastName { get; set; }
        public String EmpAddress1 { get; set; }
        public String EmpAddress2 { get; set; }
        public String EmpCity { get; set; }
        public String EmpState { get; set; }
        public String EmpZip { get; set; }
        public String EmpEmail { get; set; }
        public String EmpPhone { get; set; }
        public String EmpPassword { get; set; }
        public String Role { get; set; }
        public decimal Wage { get; set; }
        public String EmpUsername { get; set; }
    }
}