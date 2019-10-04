using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Win32.SafeHandles;


namespace Vue.Core.Data.Entities
{
    public class Users : BaseEntity
    {
        [MaxLength(20)] public string LoginName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt  { get; set; }
        
        [MaxLength(30)] public string FirstName { get; set; }
        [MaxLength(30)] public string LastName { get; set; }
        [MaxLength(100)] public string Email { get; set; }
        public Enums.Gender Gender { get; set; }
        [Column(TypeName = "Date")] public DateTime? DateOfBirth { get; set; }
        public string Comment { get; set; }
        
        #region ICollection
            public virtual  ICollection<UsersRoles> UsersRoles {get;set;}
            public virtual  ICollection<Scheduler> Scheduler {get;set;}
            public virtual  ICollection<UsersToken> UsersToken {get;set;}
        #endregion
        
        
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        
        public List<string> RolesSelected { get; set; }
      
    }
}