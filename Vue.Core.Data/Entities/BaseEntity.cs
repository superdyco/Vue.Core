using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vue.Core.Data.Entities
{
  

    public class BaseEntity:ICreatedEntity,IUpdatedEntity,IFakeDeleted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Gid { get; set; }
        public DateTime CreatedAt { get; set; }
        [MaxLength(30)] public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [MaxLength(30)] public string UpdatedBy { get; set; }
        [DefaultValue(true)] public bool IsDeleted { get; set; }
    }

    internal interface IFakeDeleted
    {
        bool IsDeleted { get; set; }
    }
    internal interface ICreatedEntity
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }

    internal interface IUpdatedEntity
    {
        DateTime UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
    
}