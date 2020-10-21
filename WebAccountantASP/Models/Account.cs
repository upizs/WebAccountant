using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAccountantASP.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
        [EnumDataType(typeof(AccountType))]
        public AccountType AccountType { get; set; }
        public double Value { get; set; }
    }
}