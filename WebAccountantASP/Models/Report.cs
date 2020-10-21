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
        
        public Account Account { get; set; }
        public double Value { get; set; }
       

    }
}