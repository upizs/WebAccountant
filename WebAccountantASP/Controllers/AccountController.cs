using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAccountantASP.Models;
using WebAccountantASP.ViewModel;

namespace WebAccountantASP.Controllers
{
    public class AccountController : Controller
    {
        #region Context

        private MyDbContext _context;

        public AccountController()
        {
            _context = new MyDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        #endregion


        // GET: Account
        public ActionResult Index()
        {
            var accounts = _context.Accounts.OrderByDescending(a => a.Value).ToList();
            var account = new Account();
            var accountTypes = Enum.GetValues(typeof(AccountType)).Cast<AccountType>().ToList();

            //set up a viewModel so that I can use a list of accounts and a single account
            var viewModel = new AddAccountViewModel
            {
                Accounts = accounts,
                Account = account,
                AccountTypes = accountTypes
                
            };

            return View(viewModel);
        }

        //Adds account to the database and refreshes index page
        [HttpPost]
        public ActionResult Add(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return RedirectToAction("Index", "Account");
        }
    }
}