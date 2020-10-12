using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using WebAccountantASP.Models;
using WebAccountantASP.ViewModel;

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
            var viewModel = new AddTransactionViewModel
            {
                Transaction = transaction,
                Transactions = transactions
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return RedirectToAction("Index", "Transaction");
        }
    }
}