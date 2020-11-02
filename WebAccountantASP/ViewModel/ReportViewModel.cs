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
        public double Diference { get; set; }
        
        public IEnumerable<Account> Accounts { get; set; }
        public ReportViewModel(IEnumerable<Report> income, IEnumerable<Report> expense, IEnumerable<Account> accounts)
        {
            Report = new Report();
            Accounts = accounts;
            IncomeReports = income;
            ExpenseReports = expense;
            IncomeSum = IncomeReports.Select(x => x.Value).Sum();
            ExpenseSum = ExpenseReports.Select(x => x.Value).Sum();
            Diference = IncomeSum - ExpenseSum;
        }
    }
}