using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vue.Core.Data.Entities
{
    public class Order : BaseEntity
    {
        public string Comment { get; set; }
        public int SchedulerId { get; set; }
        public int PatientsId { get; set; }
        [ForeignKey("SchedulerId")] public virtual Scheduler Scheduler { get; set; }
        [ForeignKey("PatientsId")] public virtual Patients Patients { get; set; }
    }
}