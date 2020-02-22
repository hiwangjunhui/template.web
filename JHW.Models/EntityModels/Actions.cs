using System;
using System.ComponentModel.DataAnnotations;

namespace JHW.Models.EntityModels
{
    public class Actions
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "区域")]
        [StringLength(50)]
        public string Area { get; set; }

        [Display(Name = "控制器名称")]
        [StringLength(50)]
        [Required]
        public string Controller { get; set; }

        [Display(Name = "Action名称")]
        [StringLength(50)]
        [Required]
        public string Action { get; set; }

        [Display(Name = "类型")]
        public int Type { get; set; }

        [Display(Name = "描述")]
        [StringLength(50)]
        public string Remark { get; set; }

        [Display(Name = "程序集Hash")]
        public byte[] AssemblyHash { get; set; }
    }
}
