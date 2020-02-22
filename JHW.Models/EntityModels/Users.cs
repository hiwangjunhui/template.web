using System;
using System.ComponentModel.DataAnnotations;

namespace JHW.Models.EntityModels
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "用户名")]
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "描述")]
        public string Remark { get; set; }
    }
}
