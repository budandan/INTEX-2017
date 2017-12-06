using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Intex_2017.Models;

namespace Intex_2017.DAL
{
    public class IntexContext: DbContext
    {
        public IntexContext() : base("IntexContext") { }
        public DbSet<Customer> Customers { get; set; }
		    public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Intex_2017.Models.CallTicket> CallTickets { get; set; }
    }
}