using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vue.Core.Data.Entities
{
    public class Scheduler : BaseEntity
    {
        [Column(TypeName = "Date")] public DateTime Date { get; set; }
        [MaxLength(5)] public string StartTime { get; set; }
        [MaxLength(5)] public string EndTime { get; set; }
        public int UsersId { get; set; }
        [DefaultValue(0)] public int Max { get; set; }
        [ForeignKey("UsersId")] public virtual Users Users { get; set; }

        #region ICollection
            public virtual  ICollection<Clinics> Clinics { get; set; }
        #endregion
    }
}