using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Vue.Core.Data.Entities
{
    public class UsersToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UsersId { get; set; }
        
        
        public string Token { get; set; }
        public DateTime? ExpiresTo { get; set; }
        
        public string RefreshToken { get; set; }
        public DateTime? RefreshExpiresTo { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        [ForeignKey("UsersId")] public virtual Users Users { get; set; }
         
    }
}