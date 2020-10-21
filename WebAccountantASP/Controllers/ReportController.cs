using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using WebAccountantASP.Models;
using WebAccountantASP.ViewModel;

namespace WebAccountantASP.Controllers
{
    public class ReportController : Controller
    {

        #region Context

        private MyDbContext _context;

        public ReportController()
        {
            _context = new MyDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion



        // GET: Report
        public ActionResult Index()
        {
            //Finds last Thursday to produce report for the last week starting last friday
            DateTime LastThurday = FindLastThursday();
            var expenseReports = new List<Report>();
            var incomeReports = new List<Report>();
            var thisWeeksTransactions = _context.Transactions.Where(x => x.Date > LastThurday).ToList();
            var accounts = _context.Accounts.ToList();

            //Find All Expense accounts in this weeks transactions
            foreach (var acc in accounts)
            {
                var isExpense = acc.AccountType == AccountType.Expense;
                //find all the transactions of this account
                var thisAccountTransactions = thisWeeksTransactions.Where(x => x.DebitId == acc.Id).ToList();
                if (isExpense && thisAccountTransactions.Any())
                {
                    var report = new Report();
                    report.Account = acc;
                    //sum the value of transactions for this account
                    report.Value += thisAccountTransactions.Select(x => x.Value).Sum();
                    expenseReports.Add(report);
                }
            }

            //Find All Income accounts in this weeks transactions
            foreach (var acc in accounts)
            {
                var isIncome = acc.AccountType == AccountType.Income;
                //find all the transactions of this account
                var thisAccountTransactions = thisWeeksTransactions.Where(x => x.CreditId == acc.Id).ToList();
                if (isIncome && thisAccountTransactions.Any())
                {
                    var report = new Report();
                    report.Account = acc;
                    //sum the value of transactions for this account
                    report.Value += thisAccountTransactions.Select(x => x.Value).Sum();
                    incomeReports.Add(report);
                }
            }




            var reportViewModel = new ReportViewModel()
            {
                Report = new Report(),
                ExpenseReports = expenseReports,
                IncomeReports = incomeReports,
                ExpenseSum = expenseReports.Select(x => x.Value).Sum(),
                IncomeSum = incomeReports.Select(x => x.Value).Sum(),
            Accounts = accounts

            };

            return View(reportViewModel);
        }


        //Finds the date of the last thursday
        public DateTime FindLastThursday()
        {
            
            var date = DateTime.Now;
            while (date.DayOfWeek != DayOfWeek.Thursday)
            {
                date = date.AddDays(-1);
            }

            return date;
        }

    }
}