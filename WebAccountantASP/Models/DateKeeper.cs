using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAccountantASP.Models
{
    public class DateKeeper
    {
        public int id { get; set; }
        //keeps the date of the last time App was started. Optional for future if I will want to keep different dates
        public DateTime? LastStarted { get; set; }
    }
}