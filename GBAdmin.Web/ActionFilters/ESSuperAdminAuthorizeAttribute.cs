using GBAdmin.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GBAdmin.Web.ActionFilters
{
    public class ESSuperAdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (SessionManager.GetSessionUser() != null)
            {
                var user = SessionManager.GetSessionUser();
                string Role = SessionManager.GetSessionRole().ToUpper();
                if (user.OrganizationID == (int)GBAdmin.Web.Services.Constants.Organizations.VS && (Role == GBAdmin.Web.Services.Constants.Roles.SuperAdmin.ToString().ToUpper()))
                    return true;

            }

            return false;
        }
    }
}