using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Vue.Core.Common;
using Vue.Core.Common.Config;
using Vue.Core.Data;
using Vue.Core.Data.Entities;
using Vue.Core.Service.Interface;

namespace Vue.Core.Service
{
    public class RolesService : IRolesService<Roles>
    {
        private ApplicationDbContext _db;
        private JwtSetting _jwtsetting;

        public RolesService(ApplicationDbContext db, IOptions<JwtSetting> jwtsetting)
        {
            _db = db;
            _jwtsetting = jwtsetting?.Value;
        }


        public IEnumerable<Roles> GetJson()
        {
            return _db.Roles;
        }

        public int Create(int userid, List<string> roles)
        {
            //新增不存在的
            foreach (var v in roles)
            {
                var rGid = new Guid(v);

                _db.UsersRoles.Add(new UsersRoles()
                {
                    UsersId = userid,
                    Roles = _db.Roles.First(x => x.Gid == rGid)
                });
            }
            return _db.SaveChanges();
        }

        public int Update(Guid usergid, List<string> roles)
        {
            var u = _db.UsersRoles.Where(x => x.Users.Gid == usergid).Select(x => new
            {
                id = x.Id,
                UserId = x.UsersId,
                RolesGid = x.Roles.Gid,
                RolesId = x.RolesId
            }).ToList();
            var usersObj = _db.Users.FirstOrDefault(x => x.Gid == usergid);
            if (usersObj == null)
                return 0;
            //新增不存在的
            foreach (var v in roles)
            {
                var rGid = new Guid(v);
                if (!u.Select((x => x.RolesGid)).Contains(rGid))
                {
                    _db.UsersRoles.Add(new UsersRoles()
                    {
                        UsersId = usersObj.Id,
                        Roles = _db.Roles.First(x => x.Gid == rGid)
                    });
                }
            }

            //移除多的
            foreach (var v in u)
            {
                if (!roles.Contains(v.RolesGid.ToString()))
                {
                   
                    var delObj = _db.UsersRoles.First(x => x.Id == v.id);
                    _db.UsersRoles.Remove(delObj);
                }
            }

            return _db.SaveChanges();
        }
    }
}