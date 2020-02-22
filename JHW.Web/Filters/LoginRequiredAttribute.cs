using JHW.Web.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace JHW.Web.Filters
{
    /// <summary>
    /// 全局登录验证
    /// </summary>
    public class LoginRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //如果不是管理端的，直接跳过本次验证
            var areaName = filterContext.RouteData.DataTokens["area"] as string;
            if (areaName != "Manager")
            {
                return;
            }

            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false))
            {
                return;
            }

            if (null == filterContext.HttpContext.Session[Utilities.ConfigBase.CurrentUserSessionKey] as LoginViewModel)
            {
                filterContext.Result = new RedirectToRouteResult("Manager_default", new RouteValueDictionary(new { controller = "Home", action = "Login", returnUrl = filterContext.HttpContext.Request.RawUrl }));
            }
        }
    }
}