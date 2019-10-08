using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vue.Core.Data.Entities
{
    public class Apps : BaseEntity
    {
        [MaxLength(20)] public string AppName { get; set; }
        [MaxLength(20)] public string RouteName { get; set; }
        [MaxLength(20)] public string IconClass { get; set; }
        [ForeignKey("Id")] public virtual int? ParentId { get; set; }
        #region ICollection
        [JsonIgnore]public virtual ICollection<RolesApps> RolesApps { get; set; }
        public virtual ICollection<AppsApiCollection> AppsApiCollection { get; set; }
        #endregion
    }
}