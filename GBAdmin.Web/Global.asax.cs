using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using NLog.Common;

namespace GBAdmin.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var routeData = new RouteData();
            var ex = Server.GetLastError().GetBaseException();
            //Added logging Error in DB
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Error("ApplicationError", ex);
            //End
            Server.ClearError();
            //var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");

            if (ex.GetType() == typeof(HttpException))
            {
                var httpException = (HttpException)ex;
                var code = httpException.GetHttpCode();
                routeData.Values.Add("status", code);
            }
            else
            {
                routeData.Values.Add("status", 500);
            }

            routeData.Values.Add("error", ex);
            IController errorController = new GBAdmin.Web.Controllers.ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

        }
    }
}
