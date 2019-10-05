using System;
using Vue.Core.Data;


namespace Vue.Core.Model
{
    public class NewsList
    {
        public Guid Gid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
}