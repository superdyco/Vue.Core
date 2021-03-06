using System;
using Vue.Core.Model;

namespace Vue.Core.Service.Interface
{
    public interface IUsersService<T> : IBaseService<T> where T : class
    {
        T Authenticate(string loginname, string password);
        T FBLogin(string accessToken);
        PagingModel<T> GetAll(Fitlers.UsersFilter filter);
        
        
    }
}