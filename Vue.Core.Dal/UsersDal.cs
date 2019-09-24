using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vue.Core.Data;
using Vue.Core.Data.Entities;

namespace Vue.Core.Dal
{
    public class UsersDal
    {
        private readonly ApplicationDbContext _db;

        public UsersDal(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 判斷是否能用此api
        /// </summary>
        /// <param name="id">使用者id</param>
        /// <param name="path">api路徑</param>
        /// <param name="policy">操作類型</param>
        /// <returns></returns>
        public bool checkApiAuth(int id, string path, Enums.ApiPolicy policy)
        {
            var data = from d in _db.Users
                join d1 in _db.UsersRoles on d.Id equals d1.UsersId
                join d2 in _db.Roles on d1.RolesId equals d2.Id
                join d3 in _db.RolesApps on d2.Id equals d3.RolesId
                join d4 in _db.Apps on d3.AppsId equals d4.Id
                join d5 in _db.AppsApiCollection on d4.Id equals d5.AppsId
                where d.Id == id && d5.RoutePath.Equals(path)
                                 && d1.IsDeleted == false && d2.IsDeleted == false && d3.IsDeleted == false
                                 && d4.IsDeleted == false && d5.IsDeleted == false
                select d3;
           
            switch (policy)
            {
                case Enums.ApiPolicy.Read:
                    data = data.Where(x => x.Read);
                    break;
                case Enums.ApiPolicy.Write:
                    data = data.Where(x => x.Read);
                    break;
                case Enums.ApiPolicy.Delete:
                    data = data.Where(x => x.Delete);
                    break;
                case Enums.ApiPolicy.Print:
                    data = data.Where(x => x.Print);
                    break;
            }
            
            return data.Any();
        }
    }
}