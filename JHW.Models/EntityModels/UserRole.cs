using System;

namespace JHW.Models.EntityModels
{
    public class UserRole
    {
        public Guid RoleId { get; set; }

        public Guid UserId { get; set; }

        public string Remark { get; set; }
    }
}
