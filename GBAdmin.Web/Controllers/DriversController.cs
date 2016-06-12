using GBAdmin.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GB.Data;
namespace GBAdmin.Web.Controllers
{
    public class DriversController : Controller
    {
        // GET: Drivers/Add
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(DriverViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
    }
}