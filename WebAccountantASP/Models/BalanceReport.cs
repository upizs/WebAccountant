using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAccountantASP.Models
{
    public class BalanceReport
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public DateTime Date { get; set; }
        public Double Value { get; set; }
    }
}