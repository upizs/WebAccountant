using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAccountantASP.Models;

namespace WebAccountantASP.Controllers
{
    public class ReportController : Controller
    {

        #region Context

        private MyDbContext _context;

        public ReportController()
        {
            _context = new MyDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion



        // GET: Report
        public ActionResult Index()
        {


            return View();
        }
    }
}