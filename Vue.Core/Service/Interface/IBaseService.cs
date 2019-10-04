using System;
using System.Collections;
using System.Collections.Generic;

namespace Vue.Core.Service
{
    public interface IBaseService<T> 
    {
        T GetById(int id);
        T GetByGId(Guid Gid);
        T Create(T t);
        void Update(T t);
        void Delete(int it);
    }
}