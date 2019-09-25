using System;

namespace Vue.Core.Service.Interface
{
    public interface IUsersTokenService
    {
        bool SaveToken(int userid, string access_token, string refresh_token,DateTime tokenExpireTo,DateTime refreshTokenExpireTo);

        bool UpdateToken(int tokenid, string access_token, string refresh_token, DateTime tokenExpireTo,
            DateTime refreshTokenExpireTo);
        int? CheckRefreshToken(string access_token, string refresh_token);
    }
}