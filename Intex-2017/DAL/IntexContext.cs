using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Intex_2017.Models;
using System.Data.SqlClient;

namespace Intex_2017.DAL
{
    public class IntexContext: DbContext
    {
        public IntexContext() : base("IntexContext") { }
        public DbSet<Customer> Customers { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CallTicket> CallTickets { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Compound> Compounds { get; set; }

        public SqlConnection con = new SqlConnection("Data Source=DESKTOP-89404G6\\SQLEXPRESS;Initial Catalog=Intex;Integrated Security=True");
        
    }

}