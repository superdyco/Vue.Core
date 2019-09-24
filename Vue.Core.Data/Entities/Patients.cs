using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vue.Core.Data.Entities
{
    public class Patients : BaseEntity
    {
        [MaxLength(30)] public string PatientId { get; set; }
        [MaxLength(30)] public string NationalId { get; set; }
        [MaxLength(30)] public string FirstName { get; set; }
        [MaxLength(30)] public string LastName { get; set; }
        [MaxLength(100)] public string Email { get; set; }
        public Enums.Gender Gender { get; set; }
        [Column(TypeName = "Date")] public DateTime? DateOfBirth { get; set; }
        public string Comment { get; set; }
        
        #region ICollection
            public virtual  ICollection<Order> Order {get;set;}
        #endregion
    }
}