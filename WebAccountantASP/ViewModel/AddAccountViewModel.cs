using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAccountantASP.Models;

namespace WebAccountantASP.ViewModel
{
    public class AddAccountViewModel
    {
        public IEnumerable<Account> Accounts { get; set; }
        public Account Account { get; set; }
        public IEnumerable<AccountType> AccountTypes { get; set; }
    }
}