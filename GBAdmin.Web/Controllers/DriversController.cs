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
using GBAdmin.Web.Services.Common;
using AutoMapper;
using System.Globalization;

namespace GBAdmin.Web.Controllers
{
    [Authorize]
    public class DriversController : Controller
    {
        GaddibabaEntities GBContext = new GaddibabaEntities();
        CommonHelper CommonHelper = new CommonHelper();
        public DriversController()
        {
            Mapper.CreateMap<DriverDetail, DriverDetailsActivityLog>();
        }

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
                    || (m.LicenceNo != null && m.LicenceNo != String.Empty && m.LicenceNo == model.LicenceNo)).FirstOrDefault();
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
                    driver.UserID = UserID;
                    driver.CreatedOn = DateTime.Now;
                    driver.LastUpdatedOn = DateTime.Now;
                    driver.DriverStatusID = 1;//for new entry its always 1
                    driver.ExpectedSalary = model.ExpectedSalary;
                    driver.CityID = model.CityID;
                    GBContext.DriverDetails.Add(driver);
                    GBContext.DriverDetailsActivityLogs.Add(Mapper.Map<DriverDetail, DriverDetailsActivityLog>(driver));
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
                    return Json(new { Success = false, Message = "Driver details(phone number or license number) already exists! Please try with some other driver." }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { Success = false, Message = "Please check your inputs and try again." }, JsonRequestBehavior.AllowGet);
        }

        //GET
        [HttpGet]
        public ActionResult List()
        {
            DriverViewModel driverViewModel = new DriverViewModel();
            var UserID = SessionManager.GetSessionUser().Id;
            /**
             * Rule:
             * 1.Super Admin,Admin,Manager and Employee can view All
             * 2.Telecaller can view only the newly entered data which are entered by his/her admin and its subordinates
             * 3.Sales people can view data entered only by him
             * */
            if (Session["Role"].ToString().ToUpper() == Constants.Roles.SuperAdmin.ToString().ToUpper()
               || Session["Role"].ToString().ToUpper() == Constants.Roles.Admin.ToString().ToUpper()
               || Session["Role"].ToString().ToUpper() == Constants.Roles.Manager.ToString().ToUpper()
               || Session["Role"].ToString().ToUpper() == Constants.Roles.Employee.ToString().ToUpper())
            {
                driverViewModel.DriverDetailsList = GBContext.DriverDetails.ToList();
            }
            
            //else if (Session["Role"].ToString().ToUpper() == Constants.Roles.Admin.ToString().ToUpper()
            //    || Session["Role"].ToString().ToUpper() == Constants.Roles.Manager.ToString().ToUpper())
            //{
            //    driverViewModel.DriverDetailsList = CommonHelper.GetDriverDetailsByUserID(UserID, Session["Role"].ToString().ToUpper());
            //}
            else if (Session["Role"].ToString().ToUpper() == Constants.Roles.Telecaller.ToString().ToUpper())
            {
                User Admin = CommonHelper.GetAdminByID(SessionManager.GetSessionUser().CreatedBy);
                if (Admin.Roles.FirstOrDefault().Name.ToUpper() == Constants.Roles.SuperAdmin.ToString().ToUpper())
                {
                    driverViewModel.DriverDetailsList = GBContext.DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.New).ToList();
                }
                else
                {
                    driverViewModel.DriverDetailsList = CommonHelper.GetDriverDetailsByUserID(Admin.UserID, Constants.Roles.Admin.ToString().ToUpper())
                    .Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.New).ToList();
                }
            }
            //else if (Session["Role"].ToString().ToUpper() == Constants.Roles.Employee.ToString().ToUpper())
            //{
            //    User Admin = CommonHelper.GetAdminByID(SessionManager.GetSessionUser().CreatedBy);
            //    if (Admin.Roles.FirstOrDefault().Name.ToUpper() == Constants.Roles.SuperAdmin.ToString().ToUpper())
            //    {
            //        driverViewModel.DriverDetailsList = GBContext.DriverDetails.Where(m => m.DriverStatusID != (int)Constants.EnumDriverStatus.New).
            //            Where(m=>m.DriverStatusID != (int)Constants.EnumDriverStatus.Rejected).ToList();
            //    }
            //    else
            //    {
            //        driverViewModel.DriverDetailsList = CommonHelper.GetDriverDetailsByUserID(Admin.UserID, Constants.Roles.Admin.ToString().ToUpper())
            //        .Where(m => m.DriverStatusID != (int)Constants.EnumDriverStatus.New).Where(m=> m.DriverStatusID != (int)Constants.EnumDriverStatus.Rejected).ToList();
            //    }
            //}
            else
            {
                driverViewModel.DriverDetailsList = GBContext.DriverDetails.Where(m => m.UserID == UserID).ToList();
            }

            driverViewModel.DriverDetailsList = driverViewModel.DriverDetailsList.OrderByDescending(x => x.CreatedOn).Where(x=>x.Uber==false).ToList();
            return View(driverViewModel);
        }

        // [HttpGet]

        [Route("Driver/Details/{ID}")]
        public ActionResult Edit(string ID)
        {
            DriverViewModel driverDetail = new DriverViewModel();
            int Id = JsonConvert.DeserializeObject<int>(ID);
            var dbDriverDetail = GBContext.DriverDetails.Where(m => m.DriverDetailID == Id).FirstOrDefault();
            driverDetail.DriverDetailID = dbDriverDetail.DriverDetailID;
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
            driverDetail.CityID = dbDriverDetail.CityID;
            driverDetail.User = dbDriverDetail.User;
            driverDetail.FollowUpOn = String.Format("{0:dd/MM/yyyy}", dbDriverDetail.FollowUpOn).Replace("-", "/") ?? String.Empty;
            driverDetail.FollowUpNotes = dbDriverDetail.FollowUpNotes;
            driverDetail.NextFollowUp = String.Format("{0:dd/MM/yyyy}", dbDriverDetail.NextFollowUp).Replace("-", "/") ?? String.Empty;
            driverDetail.DriverDetailsActivityLogs = dbDriverDetail.DriverDetailsActivityLogs.Where(x=>x.FollowUpOn!=null).OrderByDescending(x => x.FollowUpOn).ToList();
            return View(driverDetail);
        }
        [HttpPost]
        [Route("Driver/Details/{DriverDetailID}")]
        public ActionResult Edit(DriverViewModel driverDetail)
        {
            if (ModelState.IsValid)
            {

                DriverDetail dbDriverDetail = (DriverDetail)GBContext.DriverDetails.Where(m => m.DriverDetailID == driverDetail.DriverDetailID).FirstOrDefault();
                //check ph number
                if (dbDriverDetail.PhoneNumber != driverDetail.PhoneNumber)
                {
                    var User = GBContext.DriverDetails.Where(m => m.PhoneNumber == driverDetail.PhoneNumber).FirstOrDefault();
                    if (User != null)
                    {
                        return Json(new { Success = false, Message = "Phone number already exists!" }, JsonRequestBehavior.AllowGet);
                    }
                } 
                //check license number
                if (driverDetail.LicenceNo != null && driverDetail.LicenceNo != String.Empty && dbDriverDetail.LicenceNo != driverDetail.LicenceNo)
                {
                    var User = GBContext.DriverDetails.Where(m => m.LicenceNo == driverDetail.LicenceNo).FirstOrDefault();
                    if (User != null)
                    {
                        return Json(new { Success = false, Message = "License number already exists!" }, JsonRequestBehavior.AllowGet);
                    }
                }
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
                if (driverDetail.FollowUpOn != null && driverDetail.FollowUpOn != String.Empty)
                {
                    dbDriverDetail.FollowUpOn = DateTime.ParseExact(driverDetail.FollowUpOn.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                dbDriverDetail.FollowUpNotes = driverDetail.FollowUpNotes;
                if (driverDetail.NextFollowUp != null && driverDetail.NextFollowUp != String.Empty)
                {
                    dbDriverDetail.NextFollowUp = DateTime.ParseExact(driverDetail.NextFollowUp.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
               
                if (driverDetail.Status == (int)Constants.EnumDriverStatus.Attached)
                {
                    dbDriverDetail.AttachedOn = DateTime.Now;
                }
                else if (driverDetail.Status == (int)Constants.EnumDriverStatus.PartnerMatched)
                {

                    dbDriverDetail.PartnerMatchedOn = DateTime.Now;
                }
                else if (driverDetail.Status == (int)Constants.EnumDriverStatus.CompletedFirstTrip)
                {
                    dbDriverDetail.CompletedFirstTripOn = DateTime.Now;
                }
                dbDriverDetail.Ola = driverDetail.Ola;
                dbDriverDetail.Uber = driverDetail.Uber;

                GBContext.DriverDetailsActivityLogs.Add(Mapper.Map<DriverDetail, DriverDetailsActivityLog>(dbDriverDetail));
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

        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult AttachedByVS()
        {
            DriverViewModel driverViewModel = new DriverViewModel();
            driverViewModel.DriverDetailsList = GBContext.DriverDetails.Where(x => x.User.OrganizationID == (int)Constants.Organizations.VS && x.Uber == false).OrderByDescending(x=>x.CreatedOn).ToList();

            return View(driverViewModel);
        }

    }
}