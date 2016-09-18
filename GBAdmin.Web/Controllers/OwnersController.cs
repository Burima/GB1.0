using AutoMapper;
using GB.Data.DBEntity;
using GBAdmin.Web.Models;
using GBAdmin.Web.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GBAdmin.Web.Controllers
{
    [Authorize]
    public class OwnersController : Controller
    {
        
        GaddibabaEntities GBContext = new GaddibabaEntities();
      
        public OwnersController()
        {
            Mapper.CreateMap<OwnerDetail, OwnerDetailsActivityLog>();
        }

        // GET: Owners/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserID = SessionManager.GetSessionUser().Id;

                var ownerDetail = GBContext.OwnerDetails.Where(m => m.PhoneNumber == model.PhoneNumber).FirstOrDefault();
                if (ownerDetail == null)
                {
                    OwnerDetail owner = new OwnerDetail();
                    owner.FirstName = model.FirstName;
                    owner.LastName = model.LastName;
                    owner.PhoneNumber = model.PhoneNumber;
                    owner.Pincode = model.Pincode;
                    owner.UserID = UserID;
                    owner.CreatedOn = DateTime.Now;
                    owner.LastUpdatedOn = DateTime.Now;
                    owner.CityID = model.CityID;
                    GBContext.OwnerDetails.Add(owner);
                    GBContext.OwnerDetailsActivityLogs.Add(Mapper.Map<OwnerDetail, OwnerDetailsActivityLog>(owner));
                    if (GBContext.SaveChanges() > 0)
                    {
                        return Json(new { Success = true, Message = "New owner added successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "Failed to add owner." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Success = false, Message = "Owner details(phone number) already exists! Please try with some other owner." }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { Success = false, Message = "Please check your inputs and try again." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult List()
        {
            OwnerViewModel ownerViewModel = new OwnerViewModel();
            var UserID = SessionManager.GetSessionUser().Id;
            ownerViewModel.OwnerDetailsList = GBContext.OwnerDetails.ToList();
            ownerViewModel.OwnerDetailsList = ownerViewModel.OwnerDetailsList.OrderByDescending(x => x.CreatedOn).ToList();
            return View(ownerViewModel);
        }

        [Route("Owner/Details/{ID}")]
        public ActionResult Edit(string ID)
        {
            OwnerViewModel ownerDetail = new OwnerViewModel();
            int Id = JsonConvert.DeserializeObject<int>(ID);
            var dbOwnerDetail = GBContext.OwnerDetails.Where(m => m.OwnerDetailID == Id).FirstOrDefault();
            ownerDetail.OwnerDetailID = dbOwnerDetail.OwnerDetailID;
            ownerDetail.FirstName = dbOwnerDetail.FirstName;
            ownerDetail.LastName = dbOwnerDetail.LastName;
            ownerDetail.PhoneNumber = dbOwnerDetail.PhoneNumber;
            ownerDetail.Pincode = dbOwnerDetail.Pincode;
            ownerDetail.CityID = dbOwnerDetail.CityID;
            ownerDetail.User = dbOwnerDetail.User;
            ownerDetail.FollowUpOn = String.Format("{0:dd/MM/yyyy}", dbOwnerDetail.FollowUpOn).Replace("-", "/") ?? String.Empty;
            ownerDetail.FollowUpNotes = dbOwnerDetail.FollowUpNotes;
            ownerDetail.NextFollowUp = String.Format("{0:dd/MM/yyyy}", dbOwnerDetail.NextFollowUp).Replace("-", "/") ?? String.Empty;
            ownerDetail.OwnerDetailsActivityLogs = dbOwnerDetail.OwnerDetailsActivityLogs.Where(x => x.FollowUpOn != null).OrderByDescending(x => x.FollowUpOn).ToList();
            return View(ownerDetail);
        }
        [HttpPost]
        [Route("Owner/Details/{OwnerDetailID}")]
        public ActionResult Edit(OwnerViewModel ownerDetail)
        {
            if (ModelState.IsValid)
            {

                OwnerDetail dbOwnerDetail = (OwnerDetail)GBContext.OwnerDetails.Where(m => m.OwnerDetailID == ownerDetail.OwnerDetailID).FirstOrDefault();
                //check ph number
                if (dbOwnerDetail.PhoneNumber != dbOwnerDetail.PhoneNumber)
                {
                    var User = GBContext.OwnerDetails.Where(m => m.PhoneNumber == ownerDetail.PhoneNumber).FirstOrDefault();
                    if (User != null)
                    {
                        return Json(new { Success = false, Message = "Phone number already exists!" }, JsonRequestBehavior.AllowGet);
                    }
                }

                dbOwnerDetail.FirstName = ownerDetail.FirstName;
                dbOwnerDetail.LastName = ownerDetail.LastName;
                dbOwnerDetail.PhoneNumber = ownerDetail.PhoneNumber;
                dbOwnerDetail.Pincode = ownerDetail.Pincode;
                dbOwnerDetail.LastUpdatedBy = SessionManager.GetSessionUser().Id;
                dbOwnerDetail.LastUpdatedOn = DateTime.Now;
                if (ownerDetail.FollowUpOn != null && ownerDetail.FollowUpOn!= String.Empty)
                {
                    dbOwnerDetail.FollowUpOn = DateTime.ParseExact(ownerDetail.FollowUpOn.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                dbOwnerDetail.FollowUpNotes = ownerDetail.FollowUpNotes;
                if (ownerDetail.NextFollowUp != null && ownerDetail.NextFollowUp != String.Empty)
                {
                    dbOwnerDetail.NextFollowUp = DateTime.ParseExact(ownerDetail.NextFollowUp.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                GBContext.OwnerDetailsActivityLogs.Add(Mapper.Map<OwnerDetail, OwnerDetailsActivityLog>(dbOwnerDetail));
                if (GBContext.SaveChanges() > 0)
                {
                    return Json(new { Success = true, Message = "Owner Details updated Successfully!!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = false, Message = "Error in updating owner details.Please try again later." }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Success = false, Message = "Please check your inputs and try again." }, JsonRequestBehavior.AllowGet);
        }

    }
}