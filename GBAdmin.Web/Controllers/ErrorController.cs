using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GBAdmin.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int status = 0, Exception error = null)
        {
            Response.StatusCode = status;
            //return View(status);
            return RedirectToAction("LogOff", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}