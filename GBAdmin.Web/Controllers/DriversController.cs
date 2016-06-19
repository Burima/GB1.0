using GBAdmin.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GB.Data.DBEntity;
using GBAdmin.Web.Services;
namespace GBAdmin.Web.Controllers
{
    public class DriversController : Controller
    {
        GaddibabaEntities GBContext = new GaddibabaEntities();
        // GET: Drivers/Add
        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public JsonResult Add(DriverViewModel model)
        {
            int count = 0;            
            if (ModelState.IsValid)
            {              
                var UserID = SessionManager.GetSessionUser().Id;
                //long UserID = 0;
                //if (User == null)
                //{
                    
                //    var Person = GBContext.ReferePersonDetails.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);
                //    UserID = Person.ReferPersonID;

                //    if (Person == null)
                //    {
                //        ReferePersonDetail referPerson = new ReferePersonDetail();
                //        referPerson.EmailID = model.ReferPersonViewModel.Email;
                //        referPerson.PhoneNumber = model.ReferPersonViewModel.PhoneNumber;
                //        UserID = GBContext.ReferePersonDetails.Add(referPerson).ReferPersonID;
                //    }
                //}
                //else
                //{
                    //UserID = User.Id;
                //}
                var driverDetail = GBContext.DriverDetails.Where(m => m.PhoneNumber == model.PhoneNumber).FirstOrDefault();
                if (driverDetail == null)
                {
                    DriverDetail driver = new DriverDetail();
                    driver.FirstName = model.FirstName;
                    driver.LastName = model.LastName;
                    driver.PhoneNumber = model.PhoneNumber;
                    driver.Pincode = model.Pincode;
                    driver.LicenceTypeID = model.LicenceType;
                    driver.LicenceNo = model.LicenceNo;
                    driver.ExperienceInKolkata = model.ExperienceInKolkata;
                    driver.Uber = model.Uber;
                    driver.Ola = model.Ola;
                    driver.Tfs = model.Tfs;
                    driver.IsReferred = model.IsReferred;
                    driver.CreatedBy = UserID;
                    driver.CreatedOn = DateTime.Now;
                    driver.LastUpdatedOn = DateTime.Now;
                    GBContext.DriverDetails.Add(driver);

                    count = GBContext.SaveChanges();
                }
                else
                {
                    return new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new { result = "Duplicate driver details." }
                    };
                }
               
            }
            if (count > 0)
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { result = "New Driver Entry Added Successfully." }
                };
            }
            else
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { result = "Error in adding driver.Please try again later." }
                };
            }
           // return View();
        } 
       
        //GET
        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            DriverViewModel driverViewModel = new DriverViewModel();
            var UserID = SessionManager.GetSessionUser().Id;
            var driverDetailList = GBContext.DriverDetails.Where(m => m.CreatedBy == UserID).ToList();
            driverViewModel.DriverDetailsList = driverDetailList;
            return View(driverViewModel);
        }
    }
}