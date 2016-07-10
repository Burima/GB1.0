using GB.Web.BE;
using GB.Web.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GB.Web.Controllers
{
    public class HomeController : Controller
    {
        User user = new User();
        SendMail sendMail = new SendMail();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendContactUsMail(string name, string email, string phone, string message)
        {
            user.name = name;
            user.email = email;
            user.phone = phone;
            user.message = message;

            String ErrorMsg = string.Empty;
            string messagebodyPath = Server.MapPath("~/MailMessageBody.html");
            bool flag = sendMail.SendMailToAdmin(user, out ErrorMsg, messagebodyPath);

            if (flag)
            {
                return Content("Success");
            }
            else
            {
                return Content("Failed");
            }

        }

        [AllowAnonymous]
        [Route("Careers", Name = RouteNames.Careers)]
        public ActionResult Careers()
        {
            return View();
        }
    }
}
