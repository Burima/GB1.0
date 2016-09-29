using GBAdmin.Web.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBAdmin.Web.Services;
using GBAdmin.Web.Models;
using GB.Data.DBEntity;
using GBAdmin.Web.Helpers;

namespace GBAdmin.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        GB.Data.DBEntity.GaddibabaEntities GBContext = new GB.Data.DBEntity.GaddibabaEntities();
        CommonHelper CommonHelper = new CommonHelper();
        [Route("Dashboard", Name = RouteNames.Dashboard)]
        [Authorize]
        public ActionResult Index()
        {
            DashboardViewModel DashboardViewModel = new DashboardViewModel();
            List<DriverDetail> DriverDetails = new List<DriverDetail>();
            var User = SessionManager.GetSessionUser();
            var UserID = User.Id;
            var role = SessionManager.GetSessionRole().ToUpper();
            /**
            * Rule:
            * 1.Super Admin,Admin,Manager,Employee and Telecaller can view All
            * 2.Marketing people can view data entered only by him
            * */
            if (role == Constants.Roles.Marketing.ToString().ToUpper())
            {
                DriverDetails = GBContext.DriverDetails.Where(m => m.UserID == UserID).ToList();
            }
            else
            {
                DriverDetails = GBContext.DriverDetails.ToList();
            }
            //if (role == Constants.Roles.SuperAdmin.ToString().ToUpper()
            //    ||role == Constants.Roles.Admin.ToString().ToUpper()
            //    || role == Constants.Roles.Manager.ToString().ToUpper()
            //    || role == Constants.Roles.Employee.ToString().ToUpper())
            //{
            //    DriverDetails = GBContext.DriverDetails.ToList();
            //}
            
            ////else if (role == Constants.Roles.Manager.ToString().ToUpper())
            ////{
            ////    DriverDetails = CommonHelper.GetDriverDetailsByUserID(UserID, role);
            ////}
            //else if (role == Constants.Roles.Telecaller.ToString().ToUpper())
            //{
            //    User Admin = CommonHelper.GetAdminByID(SessionManager.GetSessionUser().CreatedBy);
            //    if (Admin.Roles.FirstOrDefault().Name.ToUpper() == Constants.Roles.SuperAdmin.ToString().ToUpper())
            //    {
            //        DriverDetails = GBContext.DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.New).ToList();
            //    }
            //    else
            //    {
            //        DriverDetails = CommonHelper.GetDriverDetailsByUserID(Admin.UserID, Constants.Roles.Admin.ToString().ToUpper())
            //        .Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.New).ToList();
            //    }
            //}
            ////else if (role == Constants.Roles.Employee.ToString().ToUpper())
            ////{
            ////    User Admin = CommonHelper.GetAdminByID(SessionManager.GetSessionUser().CreatedBy);
            ////    if (Admin.Roles.FirstOrDefault().Name.ToUpper() == Constants.Roles.SuperAdmin.ToString().ToUpper())
            ////    {
            ////        DriverDetails = GBContext.DriverDetails.Where(m => m.DriverStatusID != (int)Constants.EnumDriverStatus.New).Where(m=> m.DriverStatusID != (int)Constants.EnumDriverStatus.Rejected).ToList();
            ////    }
            ////    else
            ////    {
            ////        DriverDetails = CommonHelper.GetDriverDetailsByUserID(Admin.UserID, Constants.Roles.Admin.ToString().ToUpper())
            ////        .Where(m => m.DriverStatusID != (int)Constants.EnumDriverStatus.New).Where(m=> m.DriverStatusID != (int)Constants.EnumDriverStatus.Rejected).ToList();
            ////    }
            ////}
            //else
            //{

            //    DriverDetails = GBContext.DriverDetails.Where(m => m.UserID == UserID).ToList();
            //}
           
            //filtered already attached drivers
            DriverDetails = DriverDetails.Where(x => x.Uber == false).ToList();

            if (User.OrganizationID == (int)Constants.Organizations.Uber)
            {
                DriverDetails = DriverDetails.Where(x => x.isVisibletoUber == true).ToList();
            }

            DashboardViewModel.New = DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.New).Count();

            DashboardViewModel.InProgress = DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.Approved).Count() + DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.HaveLicence).Count()
                + DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.AppliedForLicence).Count() + DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.LearnerReceived).Count() +
                DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.LicenceReceived).Count() + DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.Dormant).Count();

            DashboardViewModel.Rejected = DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.Rejected).Count();

            DashboardViewModel.AttachedToUber = DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.Attached).Count()+
                DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.PartnerMatched).Count()+
                 DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.CompletedFirstTrip).Count();

            DashboardViewModel.Total = DashboardViewModel.New + DashboardViewModel.InProgress + DashboardViewModel.Rejected
                + DashboardViewModel.AttachedToUber;

            return View(DashboardViewModel);
        }

        
    }
}