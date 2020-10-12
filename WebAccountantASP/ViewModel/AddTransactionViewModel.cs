using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAccountantASP.Models;

namespace WebAccountantASP.ViewModel
{
    public class AddTransactionViewModel
    {
        public Transaction Transaction { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}