using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAccountantASP.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BalanceReport> BalanceData { get; set; }
        public DbSet<DateKeeper> DateKeeper { get; set; }
    }
}