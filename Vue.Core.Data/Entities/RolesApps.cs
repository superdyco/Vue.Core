using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vue.Core.Data.Entities
{
    public class RolesApps : BaseEntity
    {
        [DefaultValue(false)] public bool Read { get; set; }
        [DefaultValue(false)] public bool Write { get; set; }
        [DefaultValue(false)] public bool Print { get; set; }
        [DefaultValue(false)] public bool Delete { get; set; }
        
        public int? RolesId { get; set; }
        public int? AppsId { get; set; }
        [ForeignKey("RolesId")] public virtual Roles Roles { get; set; }
        [ForeignKey("AppsId")] public virtual Apps Apps { get; set; }
      
    }
}