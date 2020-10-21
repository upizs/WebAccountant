using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebAccountantASP.Models;

namespace WebAccountantASP.ViewModel
{
    public class ReportViewModel
    {
        public Report Report { get; set; }
        public IEnumerable<Report> IncomeReports { get; set; }
        public IEnumerable<Report> ExpenseReports { get; set; }
        public double IncomeSum { get; set; }
        public double ExpenseSum { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}