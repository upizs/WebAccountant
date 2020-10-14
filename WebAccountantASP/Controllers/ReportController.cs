using System;
using System.Collections.Generic;
using System.Linq;
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
            var reports = new List<Report>();
            var thisWeeksTransactions = _context.Transactions.Where(x => x.Date > LastThurday).ToList();
            var accounts = _context.Accounts.ToList();

            //Create a new report for each account in last weeks transactions and add to the list
            foreach (var acc in accounts)
            {
                if (thisWeeksTransactions.Where(x => x.DebitId == acc.Id || x.CreditId == acc.Id).Any())
                {
                    var report = new Report();
                    report.AccountId = acc.Id;
                    report.Account = acc;
                    report.DebitValue = thisWeeksTransactions.Where(x => x.DebitId == acc.Id).Select(x => x.Value).Sum();
                    report.CreditValue = thisWeeksTransactions.Where(x => x.CreditId == acc.Id).Select(x => x.Value).Sum();
                    reports.Add(report);

                }
            }


            var reportViewModel = new ReportViewModel()
            {
                Report = new Report(),
                Reports = reports,
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