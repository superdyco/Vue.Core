using System.Collections.Generic;
using System.Linq;
using Vue.Core.Data;
using Vue.Core.Service.Interface;

namespace Vue.Core.Service
{
    public class LineService:ILineService
    {
        private ApplicationDbContext _db;

        public LineService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<string> GetNews()
        {
            return _db.News.Select(x => x.Title).ToList();
        }
    }
}