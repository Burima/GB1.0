using GBAdmin.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GB.Data.DBEntity;
using GBAdmin.Web.Services;
using Newtonsoft.Json;
using GBAdmin.Web.Helpers;
namespace GBAdmin.Web.Controllers
{
    [Authorize]
    public class DriversController : Controller
    {
        GaddibabaEntities GBContext = new GaddibabaEntities();
        CommonHelper CommonHelper = new CommonHelper();
        // GET: Drivers/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]       
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
                    driver.IsReferred = model.IsReferred;
                    driver.CreatedBy = UserID;
                    driver.CreatedOn = DateTime.Now;
                    driver.LastUpdatedOn = DateTime.Now;
                    driver.DriverStatusID = 1;//for new entry its always 1
                    driver.ExpectedSalary = model.ExpectedSalary;
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
        public ActionResult List()
        {
            DriverViewModel driverViewModel = new DriverViewModel();
            var UserID =  SessionManager.GetSessionUser().Id;
          
            if (Session["Role"].ToString().ToUpper() == Constants.Roles.SuperAdmin.ToString().ToUpper())
            {
                driverViewModel.DriverDetailsList = GBContext.DriverDetails.ToList();
            }
            else if (Session["Role"].ToString().ToUpper() == Constants.Roles.Employee.ToString().ToUpper())
            {
                driverViewModel.DriverDetailsList = GBContext.DriverDetails.Where(m => m.CreatedBy == UserID).ToList();
            }
            else if (Session["Role"].ToString().ToUpper() == Constants.Roles.Admin.ToString().ToUpper()
                || Session["Role"].ToString().ToUpper() == Constants.Roles.Manager.ToString().ToUpper())
            {
                driverViewModel.DriverDetailsList = CommonHelper.GetDriverDetailsByUserID(UserID, Session["Role"].ToString().ToUpper());
            }
            
            return View(driverViewModel);
        }

       // [HttpGet]
        
        [Route("Driver/Details/{ID}")]
        public ActionResult Edit(string ID)
        {
            int Id = JsonConvert.DeserializeObject<int>(ID);
            DriverDetail DriverDetail = (DriverDetail)GBContext.DriverDetails.Where(m => m.ID == Id).FirstOrDefault();
            return View(DriverDetail);
        }
        [HttpPost]
        [Route("Driver/Details/{ID}")]
        public ActionResult Edit(DriverDetail driverDetail)
        {
            DriverDetail dbDriverDetail = (DriverDetail)GBContext.DriverDetails.Where(m => m.ID == driverDetail.ID).FirstOrDefault();
            dbDriverDetail.FirstName = driverDetail.FirstName;
            dbDriverDetail.LastName = driverDetail.LastName;
            dbDriverDetail.PhoneNumber = driverDetail.PhoneNumber;
            dbDriverDetail.Pincode = driverDetail.Pincode;
            dbDriverDetail.LicenceTypeID = driverDetail.LicenceTypeID;
            dbDriverDetail.DriverStatusID = driverDetail.DriverStatusID;
            dbDriverDetail.LicenceNo = driverDetail.LicenceNo;
            dbDriverDetail.ExperienceInKolkata = driverDetail.ExperienceInKolkata;
            dbDriverDetail.ExpectedSalary = driverDetail.ExpectedSalary;
            dbDriverDetail.LastUpdatedBy = SessionManager.GetSessionUser().Id;
            dbDriverDetail.LastUpdatedOn = DateTime.Now;
            dbDriverDetail.Ola = driverDetail.Ola;
            dbDriverDetail.Uber = driverDetail.Uber;
            int count = GBContext.SaveChanges();
            if (count > 0)
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { result = "Driver updated Successfully!!" }
                };
                              
            }
            else
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { result = "Error in updating driver.Please try again later." }
                };              

            }
           // return View();
        }

      
    }
}