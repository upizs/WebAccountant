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
        public Report Report
        {
            get
            {
                return new Report();
            }
        }
        public IEnumerable<Report> IncomeReports { get; set; }
        public IEnumerable<Report> ExpenseReports { get; set; }
        public IEnumerable<ArchiveEntry> Archives { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public double IncomeSum
        {
            get
            {
                return this.IncomeReports.Select(x => x.Value).Sum();
            }
        }
        public double ExpenseSum
        {
            get
            {
                return this.ExpenseReports.Select(x => x.Value).Sum();
            }
        }
        public double Diference
        {
            get
            {
                //Rounded because sometimes result appears to be having more than two decimal places
                return Math.Round(this. IncomeSum - this.ExpenseSum,2);
            }
        }

        
       
    }
}