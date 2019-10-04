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
    public class AppsApiCollection : BaseEntity
    {
        public string RoutePath { get; set; }
        public int? AppsId { get; set; }
        [DefaultValue(0)] public int Hits { get; set; }
        [ForeignKey("AppsId")][JsonIgnore] public virtual Apps Apps { get; set; }
       
    }
}