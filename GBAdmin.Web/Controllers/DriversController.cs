using GBAdmin.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GB.Data;
using GB.Data.DBEntity;
using GBAdmin.Web.Services;
namespace GBAdmin.Web.Controllers
{
    public class DriversController : Controller
    {
        GaddibabaEntities GBContext = new GaddibabaEntities();
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
                var User = SessionManager.GetSessionUser();
                long UserID = User.Id;
                if (User == null)
                {
                    
                    var Person = GBContext.ReferePersonDetails.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);
                    UserID = Person.ReferPersonID;

                    if (Person == null)
                    {
                        ReferePersonDetail referPerson = new ReferePersonDetail();
                        referPerson.EmailID = model.ReferPersonViewModel.Email;
                        referPerson.PhoneNumber = model.ReferPersonViewModel.PhoneNumber;
                        UserID = GBContext.ReferePersonDetails.Add(referPerson).ReferPersonID;
                    }
                }
                DriverDetail driver = new DriverDetail();
                driver.FirstName = model.FirstName;
                driver.LastName = model.LastName;
                driver.PhoneNumber = model.PhoneNumber;
                driver.Pincode = model.Pincode;
                driver.LicenceType = model.LicenceType;
                driver.LicenceNo = model.LicenceNo;
                driver.ExperienceInKolkata = model.ExperienceInKolkata;
                driver.Uber = model.Uber;
                driver.Ola = model.Ola;
                driver.Tfs = model.Tfs;
                driver.IsReferred = model.IsReferred;
                driver.UserID = UserID;
                GBContext.DriverDetails.Add(driver);
            }
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
    }
}