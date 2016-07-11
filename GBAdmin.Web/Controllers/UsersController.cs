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
        public ActionResult List()
        {
            var UserID = SessionManager.GetSessionUser().Id;
            var UserList = GBContext.Users.Where(m => m.CreatedBy == UserID).ToList();
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
            var user = SessionManager.GetSessionUser();
            if (user != null)
            {
                var userDetails = GBContext.UserDetails.Where(p => p.UserID == user.Id);
                userViewModel.ManageUserViewModel = new ManageUserViewModel();
                userViewModel.UserID = user.Id;
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

                if (userDetails != null && userDetails.Count() > 0)
                {
                    userViewModel.PresentAddress = userDetails.FirstOrDefault().PresentAddress;
                    userViewModel.PermanentAddress = userDetails.FirstOrDefault().PermanentAddress;
                    userViewModel.GovtIDType = userDetails.FirstOrDefault().GovtIDType;
                    userViewModel.GovtID = userDetails.FirstOrDefault().GovtID;
                    userViewModel.HighestEducation = userDetails.FirstOrDefault().HighestEducation;
                    userViewModel.InstitutionName = userDetails.FirstOrDefault().InstitutionName;
                }

            }
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult ViewProfile(UserViewModel userViewModel)
        {

            userViewModel.Status = 1;
            userViewModel.LastUpdatedOn = DateTime.Now;
            //if (userViewModel.UserID == 0)
            //{
            //    //UserController accountController = new UserController((UserManagement)userManagement);
            //    //accountController.Logout();

            //}

            int count = UpdateUser(userViewModel);
            if (count > 0)
            {
                var user = SessionManager.GetSessionUser();
                user.ProfilePicture = userViewModel.ProfilePicture;
                TempData["message"] = "Profile updated successfully!";
            }
            else
            {
                TempData["message"] = "Error in updating your profile.Please try again later";

            }
            return PartialView("_EditProfile", userViewModel);
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
            var dbUser = GBContext.Users.FirstOrDefault(m => m.UserID == userViewModel.UserID);
            if (dbUser != null)
            {
                dbUser.UserName = userViewModel.UserName;
                dbUser.PhoneNumber = userViewModel.PhoneNumber;
                dbUser.FirstName = userViewModel.FirstName;
                dbUser.LastName = userViewModel.LastName;
                dbUser.Gender = userViewModel.Gender;
                dbUser.ProfilePicture = String.Empty;
                dbUser.Email = userViewModel.Email;

                if (dbUser.UserDetails != null && dbUser.UserDetails.Count > 0)
                {
                    dbUser.UserDetails.FirstOrDefault().PresentAddress = userViewModel.PresentAddress;
                    dbUser.UserDetails.FirstOrDefault().PermanentAddress = userViewModel.PermanentAddress;
                    dbUser.UserDetails.FirstOrDefault().GovtIDType = userViewModel.GovtIDType;
                    dbUser.UserDetails.FirstOrDefault().GovtID = userViewModel.GovtID;
                    dbUser.UserDetails.FirstOrDefault().HighestEducation = userViewModel.HighestEducation;
                    dbUser.UserDetails.FirstOrDefault().InstitutionName = userViewModel.InstitutionName;
                    dbUser.UserDetails.FirstOrDefault().LastUpdatedOn = DateTime.Now;
                }
                else
                {
                    UserDetail userDetail = new UserDetail();
                    userDetail.PresentAddress = userViewModel.PresentAddress;
                    userDetail.PermanentAddress = userViewModel.PermanentAddress;
                    userDetail.GovtIDType = userViewModel.GovtIDType;
                    userDetail.GovtID = userViewModel.GovtID;
                    userDetail.UserProfession = userViewModel.UserProfession;
                    userDetail.OfficeLocation = userViewModel.OfficeLocation;
                    userDetail.CurrentEmployer = userViewModel.CurrentEmployer;
                    userDetail.EmployeeID = userViewModel.EmployeeID;
                    userDetail.HighestEducation = userViewModel.HighestEducation;
                    userDetail.InstitutionName = userViewModel.InstitutionName;
                    userDetail.CreatedOn = DateTime.Now;
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
