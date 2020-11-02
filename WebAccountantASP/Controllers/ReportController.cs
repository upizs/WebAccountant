using Microsoft.Ajax.Utilities;
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

        #region Properties

        

        #endregion

        // Shows This weeks Report
        public ActionResult Index()
        {
            //Finds last Thursday to produce report for the last week starting last friday
            DateTime LastFriday = FindLastFriday();
            DateTime FirstDayOfTheMonth = FindTheFirstDayOfThisMonth();


            //Get all the transactions between selected time period
            var thisWeeksTransactions = GetTransactions(LastFriday);
            var thisMonthsTransactions = GetTransactions(FirstDayOfTheMonth);

            var accounts = _context.Accounts.ToList();

            //Get this weeks reports 
            var thisWeeksExpenseReports = GetReports(AccountType.Expense, thisWeeksTransactions, accounts);
            var thisWeeksIncomeReports = GetReports(AccountType.Income, thisWeeksTransactions, accounts);

            //Get this months reports
            var thisMonthsExpenseReports = GetReports(AccountType.Expense, thisMonthsTransactions, accounts);
            var thisMonthsIncomeReports = GetReports(AccountType.Income, thisMonthsTransactions, accounts);


            //create a report viewModel
            var reportViewModel = new ReportViewModel(thisWeeksIncomeReports, thisWeeksExpenseReports, accounts);
           

            return View(reportViewModel);
        }

        //This months Report

        #region Helper Methods

        //Finds the date of the last thursday
        public DateTime FindLastFriday()
        {
            
            var date = DateTime.Now;
            while (date.DayOfWeek != DayOfWeek.Friday)
            {
                date = date.AddDays(-1);
            }

            return date;
        }

        //Finds the first date of this month
        public DateTime FindTheFirstDayOfThisMonth()
        {
            var date = DateTime.Now;
            var firstDayOfTheMonth = new DateTime(date.Year, date.Month, 1);
            return firstDayOfTheMonth;
        }

        //Gets a list of transaction from the given date
        public List<Transaction> GetTransactions(DateTime date)
        {
            var transactions = _context.Transactions.Where(x => x.Date >= date).ToList();
            return transactions;
        }

        //Creates a list of reports for each account based on given type and list
        public List<Report> GetReports (AccountType accountType, List<Transaction> transactions, List<Account> accounts)
        {
            var reports = new List<Report>();
            foreach (var acc in accounts)
            {
                var isExpense = acc.AccountType == accountType;
                var thisAccountTransactions = new List<Transaction>();

                //Find all transactions for this account in given list
                //Check different Id because of how accounting works. Expense is debit and Income is Credit
                if (accountType == AccountType.Expense)
                    thisAccountTransactions = transactions.Where(x => x.DebitId == acc.Id).ToList();
                else if(accountType == AccountType.Income)
                    thisAccountTransactions = transactions.Where(x => x.CreditId == acc.Id).ToList();
                
                
                if (isExpense && thisAccountTransactions.Any())
                {
                    var report = new Report();
                    report.Account = acc;
                    //sum the value of transactions for this account
                    report.Value += thisAccountTransactions.Select(x => x.Value).Sum();
                    reports.Add(report);
                }
            }
            return reports;

        }

        #endregion
    }
}