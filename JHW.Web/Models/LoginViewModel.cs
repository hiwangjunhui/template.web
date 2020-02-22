using System;
using System.ComponentModel.DataAnnotations;

namespace JHW.Web.Models
{
    /// <summary>
    /// 登录
    /// </summary>
    [Serializable]
    public class LoginViewModel
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
