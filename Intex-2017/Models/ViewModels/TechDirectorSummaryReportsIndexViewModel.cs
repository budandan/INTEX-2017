﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex_2017.Models.ViewModels
{
    public class TechDirectorSummaryReportsIndexViewModel
    {
        public int? SummaryReportID { get; set; }
        public int? WorkOrderID { get; set; }
        public String CustCompany { get; set; }
        public String CustFirstName { get; set; }
        public String CustLastName { get; set; }
        public DateTime DateDue { get; set; }
    }
}