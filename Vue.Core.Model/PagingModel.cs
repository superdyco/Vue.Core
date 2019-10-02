using System.Linq;

namespace Vue.Core.Model
{
    public class PagingModel<T>
    {
        public PagingModel(IQueryable<T> _items,int _recordcount)
        {
            items = _items;
            recordcount = _recordcount;
        }
        public IQueryable<T> items { get; set; }
        public int recordcount { get; set; } = 0;
    }
}