using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Vue.Core.Common.Config;
using Vue.Core.Data.Entities;

namespace Vue.Core.helps
{
    public static class TokenMan
    {
        internal static (string tokenString,DateTime expireTo) GenToken(Users user, JwtSetting setting,int expire)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keybyte = Encoding.ASCII.GetBytes(setting.Key);
            var expireTo = DateTime.UtcNow.AddMinutes(expire);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = setting.Issuer,
                Audience = setting.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //UserGuid
                    new Claim("ID", user.Id.ToString()),
                    //主體內容
                    new Claim(JwtRegisteredClaimNames.Sub, user.LoginName),
                    //唯一識別碼，是區分大小寫的字串
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //頒發時間，是數字日期
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                }),
                Expires = expireTo,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(keybyte), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token),expireTo);
        }
    }
}