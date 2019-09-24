using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vue.Core.Data.Entities
{
    public class Roles : BaseEntity
    {
        [MaxLength(20)] public string RoleName { get; set; }
        public bool Lock { get; set; }
        #region ICollection
        public virtual ICollection<RolesApps> RolesApps { get; set; }
            public virtual ICollection<UsersRoles> UsersRoles { get; set; }
        #endregion
    }
}