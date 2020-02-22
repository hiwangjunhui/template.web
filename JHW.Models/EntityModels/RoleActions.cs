using System;
using System.ComponentModel.DataAnnotations;

namespace JHW.Models.EntityModels
{
    public class RoleActions
    {
        [Key]
        public Guid RoleId { get; set; }

        public Guid ActionId { get; set; }
    }
}
