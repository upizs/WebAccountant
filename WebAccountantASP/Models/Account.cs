using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAccountantASP.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDebit { get; set; }
        public double Value { get; set; }
    }
}