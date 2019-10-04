using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vue.Core.Data.Entities
{
    public class UsersRoles : BaseEntity
    {
        public int? UsersId { get; set; }
        public int? RolesId { get; set; }
        [ForeignKey("UsersId")][JsonIgnore] public virtual Users Users { get; set; }
        [ForeignKey("RolesId")] public virtual Roles Roles { get; set; }
        
    }
}