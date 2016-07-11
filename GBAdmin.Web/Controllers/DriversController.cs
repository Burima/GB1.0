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
            if (ModelState.IsValid)
            {              
                var UserID = SessionManager.GetSessionUser().Id;
               
                var driverDetail = GBContext.DriverDetails.Where(m => m.PhoneNumber == model.PhoneNumber 
                    ||(m.LicenceNo!=null && m.LicenceNo!=String.Empty && m.LicenceNo==model.LicenceNo)).FirstOrDefault();
                if (driverDetail == null)
                {
                    DriverDetail driver = new DriverDetail();
                    driver.FirstName = model.FirstName;
                    driver.LastName = model.LastName;
                    driver.PhoneNumber = model.PhoneNumber;
                    driver.Pincode = model.Pincode;
                    driver.LicenceTypeID = model.LicenceType;
                    driver.LicenceNo = model.LicenceNo;
                    driver.Uber = model.Uber;
                    driver.Ola = model.Ola;
                    driver.IsReferred = model.IsReferred;
                    driver.CreatedBy = UserID;
                    driver.CreatedOn = DateTime.Now;
                    driver.LastUpdatedOn = DateTime.Now;
                    driver.DriverStatusID = 1;//for new entry its always 1
                    driver.ExpectedSalary = model.ExpectedSalary;
                    GBContext.DriverDetails.Add(driver);                   

                    if (GBContext.SaveChanges() > 0)
                    {
                        return Json(new { Success = true, Message = "New driver added successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "Failed to add driver." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Success = false, Message = "Driver already exists! Please try with some other driver." }, JsonRequestBehavior.AllowGet);
                }
               
            }
            return Json(new { Success = false, Message = "Please check your inputs and try again." }, JsonRequestBehavior.AllowGet);
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
           
            else if (Session["Role"].ToString().ToUpper() == Constants.Roles.Admin.ToString().ToUpper()
                || Session["Role"].ToString().ToUpper() == Constants.Roles.Manager.ToString().ToUpper())
            {
                driverViewModel.DriverDetailsList = CommonHelper.GetDriverDetailsByUserID(UserID, Session["Role"].ToString().ToUpper());
            }
            else
            {
                driverViewModel.DriverDetailsList = GBContext.DriverDetails.Where(m => m.CreatedBy == UserID).ToList();
            }
            
            return View(driverViewModel);
        }

       // [HttpGet]
        
        [Route("Driver/Details/{ID}")]
        public ActionResult Edit(string ID)
        {
            DriverViewModel driverDetail = new DriverViewModel();
            int Id = JsonConvert.DeserializeObject<int>(ID);
            var dbDriverDetail = GBContext.DriverDetails.Where(m => m.ID == Id).FirstOrDefault();
            driverDetail.ID = dbDriverDetail.ID;
            driverDetail.FirstName = dbDriverDetail.FirstName;
            driverDetail.LastName = dbDriverDetail.LastName;
            driverDetail.PhoneNumber = dbDriverDetail.PhoneNumber;
            driverDetail.Pincode = dbDriverDetail.Pincode;
            driverDetail.LicenceType = dbDriverDetail.LicenceTypeID;
            driverDetail.Status = dbDriverDetail.DriverStatusID;
            driverDetail.LicenceNo = dbDriverDetail.LicenceNo;
            driverDetail.ExpectedSalary = dbDriverDetail.ExpectedSalary;
            driverDetail.Ola = dbDriverDetail.Ola;
            driverDetail.Uber = dbDriverDetail.Uber;
            return View(driverDetail);
        }
        [HttpPost]
        [Route("Driver/Details/{ID}")]
        public ActionResult Edit(DriverViewModel driverDetail)
        {
            if (ModelState.IsValid)
            {
                DriverDetail dbDriverDetail = (DriverDetail)GBContext.DriverDetails.Where(m => m.ID == driverDetail.ID).FirstOrDefault();
                dbDriverDetail.FirstName = driverDetail.FirstName;
                dbDriverDetail.LastName = driverDetail.LastName;
                dbDriverDetail.PhoneNumber = driverDetail.PhoneNumber;
                dbDriverDetail.Pincode = driverDetail.Pincode;
                dbDriverDetail.LicenceTypeID = driverDetail.LicenceType;
                dbDriverDetail.DriverStatusID = driverDetail.Status;
                dbDriverDetail.LicenceNo = driverDetail.LicenceNo;
                dbDriverDetail.ExpectedSalary = driverDetail.ExpectedSalary;
                dbDriverDetail.LastUpdatedBy = SessionManager.GetSessionUser().Id;
                dbDriverDetail.LastUpdatedOn = DateTime.Now;
                dbDriverDetail.Ola = driverDetail.Ola;
                dbDriverDetail.Uber = driverDetail.Uber;
                
                if (GBContext.SaveChanges() > 0)
                {
                   return Json(new { Success = true, Message = "Driver Details updated Successfully!!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                   return Json(new { Success = false, Message = "Error in updating driver details.Please try again later." }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Success = false, Message = "Please check your inputs and try again." }, JsonRequestBehavior.AllowGet);
        }

      
    }
}