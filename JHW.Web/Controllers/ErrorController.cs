using System.Web.Mvc;

namespace JHW.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Attributes.ActionDescription(Name = "错误页")]
        public ActionResult Index()
        {
            return Content("报错了，大兄弟");
        }
    }
}