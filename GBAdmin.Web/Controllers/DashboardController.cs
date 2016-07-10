using GBAdmin.Web.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBAdmin.Web.Services;
using GBAdmin.Web.Models;

namespace GBAdmin.Web.Controllers
{
    public class DashboardController : Controller
    {
        GB.Data.DBEntity.GaddibabaEntities GBContext = new GB.Data.DBEntity.GaddibabaEntities();

        [Route("Dashboard", Name = RouteNames.Dashboard)]
        [Authorize]
        public ActionResult Index()
        {
            var UserID = SessionManager.GetSessionUser().Id;
            var UserList = GBContext.Users.Where(m => m.CreatedBy == UserID).ToList();
            DashboardViewModel DashboardViewModel = new DashboardViewModel();
            DashboardViewModel.New = new Dictionary<string, int>();
            DashboardViewModel.Approved = new Dictionary<string, int>();
            DashboardViewModel.Rejected = new Dictionary<string, int>();
            DashboardViewModel.AttachedToUber = new Dictionary<string, int>();
            foreach (var user in UserList)
            {
                string UserName = user.FirstName + " " + user.LastName;
                int newCount = GBContext.DriverDetails.Count(m => m.CreatedBy == user.UserID && m.DriverStatusID == 1);
                int approvedCount = GBContext.DriverDetails.Count(m => m.CreatedBy == user.UserID && m.DriverStatusID == 2);
                int rejectedCount = GBContext.DriverDetails.Count(m => m.CreatedBy == user.UserID && m.DriverStatusID == 3);
                int attachedToUberCount = GBContext.DriverDetails.Count(m => m.CreatedBy == user.UserID && m.DriverStatusID == 8);

                if (newCount > 0)
                {
                    DashboardViewModel.New.Add(new KeyValuePair<string, int>(UserName, newCount));
                }
                if (approvedCount > 0)
                {
                    DashboardViewModel.Approved.Add(new KeyValuePair<string, int>(UserName, approvedCount));
                }
                if (rejectedCount > 0)
                {
                    DashboardViewModel.Rejected.Add(new KeyValuePair<string, int>(UserName, rejectedCount));
                }
                if (attachedToUberCount > 0)
                {
                    DashboardViewModel.AttachedToUber.Add(new KeyValuePair<string, int>(UserName, attachedToUberCount));
                }
            }
            return View(DashboardViewModel);
        }

        
    }
}