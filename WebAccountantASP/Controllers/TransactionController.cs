using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using WebAccountantASP.Models;
using WebAccountantASP.ViewModel;
using System.Web.UI.WebControls;

namespace WebAccountantASP.Controllers
{
    public class TransactionController : Controller
    {
        #region context

        private MyDbContext _context;

        public TransactionController()
        {
            _context = new MyDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        #endregion


        // GET: Transaction
        public ActionResult Index()
        {

            var transactions = _context.Transactions.Include(s => s.Debit).Include(s => s.Credit).ToList();
            var transaction = new Transaction();
            var accounts = _context.Accounts.ToList();
            var viewModel = new AddTransactionViewModel
            {
                Transaction = transaction,
                Transactions = transactions,
                Accounts = accounts
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(Transaction transaction)
        {
            //Add date to transaction automaticly, no need for precise date
            transaction.Date = DateTime.Now;

            //Find the accounts debited and credited in transaction to update the value.
            var accountDebited = _context.Accounts.Single(c => c.Id == transaction.DebitId);
            var accountCredited = _context.Accounts.Single(c => c.Id == transaction.CreditId);

            //Update Debited Account, Expense Accounts gain value when debited 
            if (accountDebited.AccountType == AccountType.Debit || accountDebited.AccountType == AccountType.Expense)
                accountDebited.Value += transaction.Value;
            //Credit account loose value if debited
            else
                accountDebited.Value -= transaction.Value;

            //Update Credited Account, Credit account gains value when credited and Income gains value.
            if (accountCredited.AccountType == AccountType.Credit || accountCredited.AccountType == AccountType.Income)
                accountCredited.Value += transaction.Value;
            else
                accountCredited.Value -= transaction.Value;


            //Add the transaction to database and save
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            //redirect to the same page to refresh the page
            return RedirectToAction("Index", "Transaction");
        }

    }
}