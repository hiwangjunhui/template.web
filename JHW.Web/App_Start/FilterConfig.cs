using System.Web;
using System.Web.Mvc;
using JHW.Web.Filters;

namespace JHW.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new LoginRequiredAttribute());
            filters.Add(new LogOperationAttribute());
        }
    }
}
