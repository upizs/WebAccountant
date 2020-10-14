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
        public Report Report { get; set; }
        public IEnumerable<Report> Reports { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}