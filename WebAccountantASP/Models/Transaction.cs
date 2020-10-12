using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAccountantASP.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int? DebitId { get; set; }
        [ForeignKey("DebitId")]
        public Account Debit { get; set; }
        public int? CreditId { get; set; }
        [ForeignKey("CreditId")]
        public Account Credit { get; set; }
        [Required]
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}