using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GB.Membership;

namespace GBAdmin.Web.Services
{
    public class SessionManager
    {
        public static void SessionizeUser(User User)
        {
            System.Web.HttpContext.Current.Session["User"] = User;
        }

        public static void DeSessionizeUser()
        {
            System.Web.HttpContext.Current.Session["User"] = null;
        }

        public static User GetSessionUser()
        {
            return (User)System.Web.HttpContext.Current.Session["User"];
        }
    }
}