using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models
{
    public class WorkOrder
    {
        public int WorkOrderID { get; set; }
        public String Comments { get; set; }
        public DateTime DateArrived { get; set; }
        public decimal ClientQuantity { get; set; }
        public String ReceivedByWho { get; set; }
        public DateTime DateDue { get; set; }
        public decimal Weight { get; set; }
        public int CustID { get; set; }
        public int SalesAgentID { get; set; }
        public decimal ActualQuantity { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsVerified { get; set; }
    }
}