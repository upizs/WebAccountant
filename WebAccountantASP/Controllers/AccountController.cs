using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            //I collect all the accounts separeted by their AccountType so I can see my totals
            var debitAccounts = _context.Accounts.Where(x => x.AccountType == AccountType.Debit).ToList();
            var creditAccounts = _context.Accounts.Where(x => x.AccountType == AccountType.Credit).ToList();
            //Expense and Income are together because they are not Balance Accounts, so their totals dont matter in account page. 
            var expenseIncomeAccounts = _context.Accounts.Where(x => x.AccountType == AccountType.Income || x.AccountType == AccountType.Expense).ToList();
            //creating an empty account for the form
            var account = new Account();
            var accountTypes = Enum.GetValues(typeof(AccountType)).Cast<AccountType>().ToList();

            //set up a viewModel so that I can use a list of accounts and a single account
            var viewModel = new AccountFormViewModel
            {
                DebitAccounts = debitAccounts,
                CreditAccounts = creditAccounts,
                ExpenseIncomeAccounts = expenseIncomeAccounts,
                Account = account,
                AccountTypes = accountTypes
                
            };

            return View(viewModel);
        }

        //Adds account to the database and refreshes index page
        [HttpPost]
        public ActionResult Save(Account account)
        {
            if (account.Id == 0)
            {
                _context.Accounts.Add(account);
            }
            else
            {
                var accountInDb = _context.Accounts.Single(a => a.Id == account.Id);

                //using this approuch for security reasons in case somoene doesnt have full access.
                accountInDb.Name = account.Name;
                accountInDb.AccountType = account.AccountType;
                accountInDb.Value = account.Value;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Account");
        }

        //Edit account details
        //[HttpPost]
        public ActionResult Edit(int id)
        {
            var account = _context.Accounts.SingleOrDefault(a => a.Id == id);
            var accountTypes = Enum.GetValues(typeof(AccountType)).Cast<AccountType>().ToList();

            if (account == null)
            {
                return HttpNotFound();
            }

            var viewModel = new AccountFormViewModel()
            {
                Account = account,
                AccountTypes = accountTypes
            };

            return View("Index", viewModel);
        }

        
        public ActionResult DeleteAccount(int id)
        {
            var account = _context.Accounts.SingleOrDefault(a => a.Id == id);
            _context.Accounts.Remove(account);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        

        
    }
}