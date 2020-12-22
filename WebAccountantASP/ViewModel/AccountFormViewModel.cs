using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAccountantASP.Models;

namespace WebAccountantASP.ViewModel
{
    public class AccountFormViewModel
    {
        [Display(Name = "Debit Accounts")]
        public IEnumerable<Account> DebitAccounts { get; set; }
        [Display(Name = "Total")]
        public double TotalDebit
        {
            get
            {
                return this.DebitAccounts.Select(x => x.Value).Sum();
            }
        }
        [Display(Name = "Credit Accounts")]
        public IEnumerable<Account> CreditAccounts { get; set; }
        [Display(Name = "Total")]
        public double TotalCredit
        {
            get
            {
                return this.CreditAccounts.Select(x => x.Value).Sum();
            }
        }
        [Display(Name = "Expense and Income Accounts")]
        public IEnumerable<Account> ExpenseIncomeAccounts { get; set; }
        public Account Account { get; set; }
        public IEnumerable<AccountType> AccountTypes { get; set; }
    }
}