using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAccountantASP.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public double DebitValue { get; set; }
        public double CreditValue { get; set; }

    }
}