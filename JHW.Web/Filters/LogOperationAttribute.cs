using System.Linq;
using System.Web.Mvc;

namespace JHW.Web.Filters
{
    /// <summary>
    /// 全局请求日志
    /// </summary>
    public class LogOperationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var parameters = request.QueryString.AllKeys.ValueOrEmpty().Union(request.Form.AllKeys.ValueOrEmpty()).Select(key => $"{key}={request[key]}");
            var cookies = request.Cookies.AllKeys.Select(key =>
            {
                var cookie = request.Cookies[key];
                return $"{cookie.Name}={cookie.Value}";
            });

            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            var clientip = request.ServerVariables["HTTP_X_FORWARDED_FOR"]?.Split(',')?.FirstOrDefault() ?? request.ServerVariables["REMOTE_ADDR"] ?? request.UserHostAddress;
            var log = DependencyResolver.Current.GetService<ILog.ILog>();
            var message = $"url: {request.RawUrl}\r\n{nameof(clientip)}: {clientip}\r\n{nameof(parameters)}:\r\n\t{string.Join(", ", parameters)}\r\n{nameof(cookies)}:\r\n\t{string.Join(", ", cookies)}\r\n";
            log.Write(message);
        }
    }
}