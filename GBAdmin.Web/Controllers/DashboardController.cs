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
    public class DashboardController : Controller
    {
        GB.Data.DBEntity.GaddibabaEntities GBContext = new GB.Data.DBEntity.GaddibabaEntities();
        CommonHelper CommonHelper = new CommonHelper();
        [Route("Dashboard", Name = RouteNames.Dashboard)]
        [Authorize]
        public ActionResult Index()
        {
            DashboardViewModel DashboardViewModel = new DashboardViewModel();
            List<DriverDetail> DriverDetails = CommonHelper.GetDriverDetailsByUserID(
                SessionManager.GetSessionUser().Id,Session["Role"].ToString() );
            DashboardViewModel.New = DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.New).Count();

            DashboardViewModel.InProgress = DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.Approved).Count() + DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.HaveLicence).Count()
                + DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.AppliedForLicence).Count() + DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.LearnerReceived).Count() +
                DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.LicenceReceived).Count();

            DashboardViewModel.Rejected = DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.Rejected).Count();

            DashboardViewModel.AttachedToUber = DriverDetails.Where(m => m.DriverStatusID == (int)Constants.EnumDriverStatus.AttachedtoUber).Count();

            DashboardViewModel.Total = DashboardViewModel.New + DashboardViewModel.InProgress + DashboardViewModel.Rejected
                + DashboardViewModel.AttachedToUber;

            return View(DashboardViewModel);
        }

        
    }
}