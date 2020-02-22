using JHW.Web.Attributes;
using JHW.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace JHW.Web.Areas.Manager.Controllers
{
    public class HomeController : Controller
    {
        private IService.IService<JHW.Models.EntityModels.Users> _service;

        public HomeController(IService.IService<JHW.Models.EntityModels.Users> service)
        {
            _service = service;
        }
        
        [ActionDescription(Name = "首页")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        [ActionDescription(Name = "登录")]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            model.Password = model.Password.ToMD5String();
            var result = _service.Select(s => s.UserName == model.UserName && s.Password == model.Password);

            if (result?.Any() ?? false)
            {
                Session[Utilities.ConfigBase.CurrentUserSessionKey] = model;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "用户或密码错误");
            return View(model);
        }
    }
}