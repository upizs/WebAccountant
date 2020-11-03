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

            //Get all the transactions and sort them by year and month
            var transactions = _context.Transactions.ToList();

            var archivedTransactions = ArchiveTransactions(transactions);

            //Get all the transactions between selected time period
            var thisWeeksTransactions = GetTransactions(LastFriday, transactions);

            var accounts = _context.Accounts.ToList();

            //Get this weeks reports 
            var thisWeeksExpenseReports = GetReports(AccountType.Expense, thisWeeksTransactions, accounts);
            var thisWeeksIncomeReports = GetReports(AccountType.Income, thisWeeksTransactions, accounts);


            //create a report viewModel
            var reportViewModel = new ReportViewModel()
            {
                Accounts = accounts,
                IncomeReports = thisWeeksIncomeReports,
                ExpenseReports = thisWeeksExpenseReports,
                Archives = archivedTransactions
            };
           

            return View(reportViewModel);
        }

        public ActionResult MontlyReport(int year, int month)
        {
            var transactions = _context.Transactions.ToList();
            var accounts = _context.Accounts.ToList();

            var monthlyTransactions = GetMonthlyTransactions(transactions, month, year);

            var monthlyExpenseReports = GetReports(AccountType.Expense, monthlyTransactions, accounts);
            var montlyIncomeReports = GetReports(AccountType.Income, monthlyTransactions, accounts);

            var reportViewModel = new ReportViewModel()
            {
                Accounts = accounts,
                IncomeReports = montlyIncomeReports,
                ExpenseReports = monthlyExpenseReports
            };

            return View(reportViewModel);
        }


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

        public List<Transaction> GetMonthlyTransactions(List<Transaction> transactions, int month, int year)
        {
            var montlyTransactions = transactions.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();
            return montlyTransactions;
        }


        //Gets a list of transaction from the given date
        public List<Transaction> GetTransactions(DateTime date, List<Transaction> transactions)
        {
            var selectedTransactions = transactions.Where(x => x.Date >= date).ToList();
            return selectedTransactions;
        }

        //Creates a list of reports for each account based on given type and list
        public List<Report> GetReports (AccountType accountType, List<Transaction> transactions, List<Account> accounts)
        {
            var reports = new List<Report>();
            foreach (var acc in accounts)
            {
                var isRightType = acc.AccountType == accountType;
                var thisAccountTransactions = new List<Transaction>();

                //Find all transactions for this account in given list
                //Check either Debit or Credit Id needs to be used because of how accounting works. Expense is debit and Income is Credit
                if (accountType == AccountType.Expense)
                    thisAccountTransactions = transactions.Where(x => x.DebitId == acc.Id).ToList();
                else if(accountType == AccountType.Income)
                    thisAccountTransactions = transactions.Where(x => x.CreditId == acc.Id).ToList();
                
                //if is rights account type and has transactions in given period, create report model for it
                if (isRightType && thisAccountTransactions.Any())
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

        //Gets transactions and archives them by year and month in descending order
        public List<ArchiveEntry> ArchiveTransactions(List<Transaction> transactions)
        {
            var archived = transactions.GroupBy(x => new
            {
                Month = x.Date.Month,
                Year = x.Date.Year
            })
                //create new archive entry for each group
                .Select(o => new ArchiveEntry
                {
                    Month = o.Key.Month,
                    Year = o.Key.Year
                })
                .OrderByDescending(a => a.Year)
                .ThenByDescending(a => a.Month)
                .ToList();
            return archived;
        }

        #endregion
    }
}