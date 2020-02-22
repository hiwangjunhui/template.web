using System;
using System.ComponentModel.DataAnnotations;

namespace JHW.Models.EntityModels
{
    public class Roles
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "角色名称")]
        [StringLength(50)]
        [Required]
        public string RoleName { get; set; }

        [Display(Name = "描述")]
        [StringLength(50)]
        public string Remark { get; set; }
    }
}
