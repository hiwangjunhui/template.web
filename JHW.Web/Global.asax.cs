using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JHW.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            TypeContainers.Container.SetDependencyResolver();
            DependencyResolver.Current.GetService<ILog.ILog>()?.Config();

            ActionConfig.ConfigActions();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            DependencyResolver.Current.GetService<ILog.ILog>()?.Write(exception); //记录日志信息 
            var httpContext = ((MvcApplication)sender).Context;
            httpContext.ClearError();

            #region 重定向到错误处理控制器
            var routeDic = new RouteValueDictionary
            {
                {"controller", "Error"},
                {"action", "Index" }
            };
            httpContext.Response.RedirectToRoute("Default", routeDic);
            #endregion
        }
    }
}
