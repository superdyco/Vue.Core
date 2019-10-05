using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vue.Core.Data.Entities
{
    public class News : BaseEntity
    {
        [MaxLength(100)] public string Title { get; set; }
        public string Content { get; set; }
        [Column(TypeName = "Date")] public DateTime StartDate { get; set; }
        [Column(TypeName = "Date")] public DateTime EndDate { get; set; }
    }
}