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
            
            DateTime LastThurday = FindLastThursday();
            var thisWeeksTransactions = _context.Transactions.Where(x => x.Date > LastThurday).ToList() ;

            var reportViewModel = new ReportViewModel()
            {
                Transaction = new Transaction(),
                Transactions = thisWeeksTransactions

            };

            return View(reportViewModel);
        }

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