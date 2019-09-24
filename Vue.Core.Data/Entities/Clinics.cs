using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vue.Core.Data.Entities
{
    public class Clinics : BaseEntity
    {
        [MaxLength(10)] public string ClinicNo { get; set; }
        [MaxLength(20)] public string ClinicName { get; set; }
        public int? SchedulerId { get; set; }
        [ForeignKey("SchedulerId")] public virtual Scheduler Scheduler { get; set; }
    }
}