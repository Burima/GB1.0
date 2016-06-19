using GBAdmin.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GB.Data.DBEntity;
using Newtonsoft.Json;

namespace GBAdmin.Web.Controllers
{
    public class UsersController : Controller
    {
        GB.Data.DBEntity.GaddibabaEntities GBContext = new GB.Data.DBEntity.GaddibabaEntities();
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

        public ActionResult Edit()
        {
            User user = (User)TempData["user"];
            return View(user);
        }
    }
}
