using System.Web.Mvc;
using System.Web.Routing;

namespace JHW.Web.Filters
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var log = DependencyResolver.Current.GetService<ILog.ILog>();
            log?.Write(filterContext.Exception);
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Error", action = "Index" }));
        }
    }
}