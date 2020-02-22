using System.Web.Mvc;

namespace JHW.Web.Controllers
{
    public class HomeController : Controller
    {
        [Attributes.ActionDescription(Name = "首页")]
        public ActionResult Index()
        {
            return View();
        }
    }
}