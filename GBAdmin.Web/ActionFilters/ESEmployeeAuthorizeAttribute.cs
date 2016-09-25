using GBAdmin.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GBAdmin.Web.ActionFilters
{
    public class ESEmployeeAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (SessionManager.GetSessionUser() != null)
            {
                var user = SessionManager.GetSessionUser();
                string Role = SessionManager.GetSessionRole().ToUpper();
                if (user.OrganizationID == (int)GBAdmin.Web.Services.Constants.Organizations.VS && (Role == GBAdmin.Web.Services.Constants.Roles.SuperAdmin.ToString().ToUpper() || Role == GBAdmin.Web.Services.Constants.Roles.Admin.ToString().ToUpper() || Role == GBAdmin.Web.Services.Constants.Roles.Manager.ToString().ToUpper() || Role == GBAdmin.Web.Services.Constants.Roles.Employee.ToString().ToUpper()))
                    return true;

            }

            return false;
        }
    }
}