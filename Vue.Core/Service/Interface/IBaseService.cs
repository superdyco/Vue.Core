using System.Collections.Generic;

namespace Vue.Core.Service
{
    public interface IBaseService<T> 
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Create(T t);
        void Update(T t);
        void Delete(int it);
    }
}