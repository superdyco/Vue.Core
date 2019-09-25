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
    public class UsersTokenService : IUsersTokenService
    {
        private ApplicationDbContext _db;
        private JwtSetting _jwtsetting;
        public UsersTokenService(ApplicationDbContext db,IOptions<JwtSetting> jwtsetting)
        {
            _db = db;
            _jwtsetting = jwtsetting?.Value;
        }

        public bool SaveToken(int userid, string access_token, string refresh_token,DateTime tokenExpireTo,DateTime refreshTokenExpireTo)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == userid);
            if (user == null) return false;
            _db.UsersToken.Add(new UsersToken
            {
                UsersId = userid,
                Token = access_token,
                ExpiresTo = tokenExpireTo,
                RefreshToken = refresh_token,
                RefreshExpiresTo = refreshTokenExpireTo
            });
            _db.SaveChanges();
            return true;
        }
        
        public bool UpdateToken(int tokenid, string access_token, string refresh_token,DateTime tokenExpireTo,DateTime refreshTokenExpireTo)
        {
            var tokenobj = _db.UsersToken.SingleOrDefault(x => x.Id == tokenid);
            if (tokenobj == null) return false;
            tokenobj.Token = access_token;
            tokenobj.ExpiresTo = tokenExpireTo;
            tokenobj.RefreshToken = refresh_token;
            tokenobj.RefreshExpiresTo = refreshTokenExpireTo;
            tokenobj.RefreshTimes += 1;
            _db.SaveChanges();
            return true;
        }
        
        /// <summary>
        /// 判斷refreshToken是否存在,且是否過期
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public int? CheckRefreshToken(string access_token, string refresh_token)
        {
            return _db.UsersToken.Where(x => x.Token == access_token && x.RefreshToken == refresh_token
                                    && x.RefreshExpiresTo>=System.DateTime.Now).Select(x=>x.UsersId).FirstOrDefault();
        }
      
    }
}