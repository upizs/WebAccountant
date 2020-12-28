using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAccountantASP.Models;


namespace WebAccountantASP.Controllers
{

    public class HomeController : Controller
    {

        #region Context

        private MyDbContext _context;

        public HomeController()
        {
            _context = new MyDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        #endregion




        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult RecordBalance()
        {
            RecordBalanceData();
            return View("Index");
        }

        public void RecordBalanceData()
        {
            var CreditDebitAccounts = _context.Accounts.Where(x => x.AccountType == AccountType.Credit || x.AccountType == AccountType.Debit).ToList();
            foreach (var acc in CreditDebitAccounts)
            {
                var balanceReport = new BalanceReport
                {
                    AccountId = acc.Id,
                    Date = DateTime.Now,
                    Value = acc.Value
                };
                _context.BalanceData.Add(balanceReport);
            }
            _context.SaveChanges();
        }


        
    }
}