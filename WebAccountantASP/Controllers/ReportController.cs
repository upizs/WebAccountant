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
            //Get the date of the last Friday, because I am getting paid on fridays
            //and I would like my short term Reports to be from Friday to Thursday
            DateTime LastFridayDate = GetLastFriday();

            //Archive all transactions in months and years to create a report navigation bar
            //The user then can view transactions by months and years
            var archivedTransactions = ArchiveTransactions();

            //Get all the transactions since last friday
            //This is for the weekly report, only available for this week for now
            var thisWeeksTransactions = FilterTransactions(LastFridayDate);

            //Get the list of all accounts for the view
            var accounts = _context.Accounts.ToList();

            //Create this weeks reports 
            var thisWeeksExpenseReports = CreateReports(AccountType.Expense, thisWeeksTransactions, accounts);
            var thisWeeksIncomeReports = CreateReports(AccountType.Income, thisWeeksTransactions, accounts);


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
            var accounts = _context.Accounts.ToList();
            var archivedTransactions = ArchiveTransactions();

            var monthlyTransactions = GetMonthlyTransactions(month, year);

            var monthlyExpenseReports = CreateReports(AccountType.Expense, monthlyTransactions, accounts);
            var montlyIncomeReports = CreateReports(AccountType.Income, monthlyTransactions, accounts);

            var reportViewModel = new ReportViewModel()
            {
                Accounts = accounts,
                IncomeReports = montlyIncomeReports,
                ExpenseReports = monthlyExpenseReports,
                Archives = archivedTransactions
                
            };

            //use the same view as no need for different
            return View("Index", reportViewModel);
        }


        #region Helper Methods

        //Gets the date of the Last Friday for this weeks report
        public DateTime GetLastFriday()
        {
            
            var date = DateTime.Now.Date;
            while (date.DayOfWeek != DayOfWeek.Friday)
            {
                date = date.AddDays(-1);
            }

            return date;
        }

        public List<Transaction> GetMonthlyTransactions(int month, int year)
        {
            var montlyTransactions = _context.Transactions.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();
            return montlyTransactions;
        }


        //Gets a list of transaction from the given date
        public List<Transaction> FilterTransactions(DateTime date)
        {
            var selectedTransactions = _context.Transactions.Where(x => x.Date >= date).ToList();
            return selectedTransactions;
        }

        //Creates a list of reports for each account based on given type and list of transactions
        public List<Report> CreateReports (AccountType accountType, List<Transaction> transactions, List<Account> accounts)
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
        public List<ArchiveEntry> ArchiveTransactions()
        {
            var archived = _context.Transactions.GroupBy(x => new
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