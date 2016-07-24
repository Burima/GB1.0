using GBAdmin.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GB.Data.DBEntity;
using Newtonsoft.Json;
using GBAdmin.Web.Models;
using System.Net;
using GBAdmin.Web.Helpers;
using GBAdmin.Web.Services.Common;

namespace GBAdmin.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        GB.Data.DBEntity.GaddibabaEntities GBContext = new GB.Data.DBEntity.GaddibabaEntities();
        UserViewModel userViewModel = new UserViewModel();
        CommonHelper CommonHelper = new CommonHelper();
        public ActionResult List()
        {
            var UserID = SessionManager.GetSessionUser().Id;
            var UserList = new List<User>();
            if (Session["Role"].ToString().ToUpper() == Constants.Roles.SuperAdmin.ToString().ToUpper())
            {
                UserList = GBContext.Users.ToList();
            }
            else
            {
                UserList = CommonHelper.GetEmployeeByUserID(UserID, Session["Role"].ToString().ToUpper());
            }
            return View(UserList);
            
        }
        [HttpPost]
        public ActionResult GetUserByID(string UserID)
        {
            long userid = JsonConvert.DeserializeObject<long>(UserID);
            var User = GBContext.Users.Where(m => m.UserID == userid).FirstOrDefault();
            TempData["user"] = User;
            return RedirectToAction("Edit", "Users");
        }

        [Route("Profile", Name = RouteNames.Profile)]
        public ActionResult Profile()
        {
            var UserID = SessionManager.GetSessionUser().Id;
            var user = GBContext.Users.Where(p => p.UserID == UserID).FirstOrDefault();
            if (user != null)
            {
                userViewModel.ManageUserViewModel = new ManageUserViewModel();
                userViewModel.PhoneNumber = user.PhoneNumber;
                userViewModel.FirstName = user.FirstName;
                userViewModel.LastName = user.LastName;
                userViewModel.Gender = user.Gender;
                userViewModel.ProfilePicture = user.ProfilePicture;
                userViewModel.ManageUserViewModel.OldPassword = user.PasswordHash;
                userViewModel.Email = user.Email;
                userViewModel.EmailConfirmed = user.EmailConfirmed;
                userViewModel.PhoneNumber = user.PhoneNumber;
                userViewModel.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                userViewModel.UserName = user.UserName;
                userViewModel.CityID = user.CityID;

                if (user.UserDetails != null && user.UserDetails.Count() > 0)
                {
                    userViewModel.PresentAddress = user.UserDetails.FirstOrDefault().PresentAddress;
                    userViewModel.PermanentAddress = user.UserDetails.FirstOrDefault().PermanentAddress;
                    userViewModel.GovtIDType = user.UserDetails.FirstOrDefault().GovtIDType;
                    userViewModel.GovtID = user.UserDetails.FirstOrDefault().GovtID;
                    userViewModel.HighestEducation = user.UserDetails.FirstOrDefault().HighestEducation;
                    userViewModel.InstitutionName = user.UserDetails.FirstOrDefault().InstitutionName;
                }

            }
            return View(userViewModel);
        }

        [HttpPost]
        public JsonResult ViewProfile(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                userViewModel.Status = 1;

                int count = UpdateUser(userViewModel);
                if (count > 0)
                {
                   return Json(new { Success = true, Message = "Profile updated successfully!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = false, Message = "Error in updating your profile.Please try again later" }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Success = false, Message = "Please check your inputs." }, JsonRequestBehavior.AllowGet);
        }

        //[Route("CropImage", Name = RouteNames.CropImage)]
        public virtual ActionResult CropImage(string imagePath, decimal? cropPointX, decimal? cropPointY, decimal? imageCropWidth,
            decimal? imageCropHeight, string fileName)
        {
            if (string.IsNullOrEmpty(imagePath) || !cropPointX.HasValue || !cropPointY.HasValue || !imageCropWidth.HasValue || !imageCropHeight.HasValue)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            byte[] imageBytes = null;
            string[] imageUriPart = imagePath.Split(',');
            string base64String = imageUriPart[1];
            imageBytes = Convert.FromBase64String(base64String);
            byte[] croppedImage = ImageHelper.CropImage(imageBytes, (int)cropPointX.Value, (int)cropPointY.Value, (int)imageCropWidth.Value, (int)imageCropHeight.Value);

            if (!string.IsNullOrEmpty(fileName))
            {
                string[] getID = fileName.Split('_');
                string tempFolderName = Server.MapPath("~/files/croppedImages/" + getID[0]);
                DateTime timestamp = DateTime.Now;
                string filename = getID[0] + String.Format("{0:d-M-yyyy HH-mm-ss}", timestamp);
                FileHelper.SaveFile(croppedImage, tempFolderName, filename);

                string photoPath = string.Concat("../files/croppedImages/", "/" + getID[0], "/" + filename + ".png");

                int count = UpdateProfilePicture(SessionManager.GetSessionUser().Id, photoPath);
                if (count > 0)
                {

                    return Json(new { PhotoPath = photoPath, filename = filename + ".png" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
        }

        private int UpdateUser(UserViewModel userViewModel)
        {
            var UserID = SessionManager.GetSessionUser().Id;
            var dbUser = GBContext.Users.FirstOrDefault(m => m.UserID == UserID);
            if (dbUser != null)
            {
                dbUser.PhoneNumber = userViewModel.PhoneNumber;
                dbUser.FirstName = userViewModel.FirstName;
                dbUser.LastName = userViewModel.LastName;
                dbUser.Gender = userViewModel.Gender;
                dbUser.ProfilePicture = String.Empty;
                dbUser.Email = userViewModel.Email;
                dbUser.CityID = userViewModel.CityID;
                dbUser.LastUpdatedBy = userViewModel.UserID;
                dbUser.LastUpdatedOn = DateTime.Now;

                if (dbUser.UserDetails != null && dbUser.UserDetails.Count > 0)
                {
                    dbUser.UserDetails.FirstOrDefault().PresentAddress = userViewModel.PresentAddress;
                    dbUser.UserDetails.FirstOrDefault().PermanentAddress = userViewModel.PermanentAddress;
                    dbUser.UserDetails.FirstOrDefault().GovtIDType = userViewModel.GovtIDType;
                    dbUser.UserDetails.FirstOrDefault().GovtID = userViewModel.GovtID;
                    dbUser.UserDetails.FirstOrDefault().HighestEducation = userViewModel.HighestEducation;
                    dbUser.UserDetails.FirstOrDefault().InstitutionName = userViewModel.InstitutionName;
                    dbUser.UserDetails.FirstOrDefault().LastUpdatedOn = DateTime.Now;
                    dbUser.UserDetails.FirstOrDefault().LastUpdatedBy = userViewModel.UserID;
                }
                else
                {
                    UserDetail userDetail = new UserDetail();
                    userDetail.PresentAddress = userViewModel.PresentAddress;
                    userDetail.PermanentAddress = userViewModel.PermanentAddress;
                    userDetail.GovtIDType = userViewModel.GovtIDType;
                    userDetail.GovtID = userViewModel.GovtID;
                    userDetail.HighestEducation = userViewModel.HighestEducation;
                    userDetail.InstitutionName = userViewModel.InstitutionName;
                    userDetail.CreatedOn = DateTime.Now;
                    userDetail.LastUpdatedOn = DateTime.Now;

                    userDetail.CreatedBy = userViewModel.UserID;
                    userDetail.LastUpdatedBy = userViewModel.UserID;
                    dbUser.UserDetails = new List<UserDetail>();
                    dbUser.UserDetails.Add(userDetail);
                    GBContext.UserDetails.Add(userDetail);
                }
            }

           return GBContext.SaveChanges();
        }

        private int UpdateProfilePicture(long UserID, string ProfilePicture)
        {
            var dbUser = GBContext.Users.FirstOrDefault(m => m.UserID == UserID);
            dbUser.ProfilePicture = ProfilePicture;
            return GBContext.SaveChanges();
        }

    }
}
