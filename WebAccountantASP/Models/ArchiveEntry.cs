using System.Globalization;

namespace WebAccountantASP.Models
{
    public class ArchiveEntry
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.Month);
            }
        }
    }
}