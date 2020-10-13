using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebAccountantASP.Models;

namespace WebAccountantASP.ViewModel
{
    public class ReportViewModel
    {
        public Transaction Transaction { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}