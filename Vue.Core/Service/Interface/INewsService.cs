using System;
using Vue.Core.Model;

namespace Vue.Core.Service.Interface
{
    public interface INewsService<T> : IBaseService<T> where T : class
    {
        PagingModel<T> GetAll(Fitlers.NewsFilter filter);
        
    }
}