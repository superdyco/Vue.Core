using System;
using System.Collections.Generic;
using Vue.Core.Model;

namespace Vue.Core.Service.Interface
{
    public interface IRolesService<T>
    {
        IEnumerable<T> GetJson();
        int Create(int userid, List<string> roles);
        int Update(Guid usergid, List<string> roles);
    }
}