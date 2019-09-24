using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using Vue.Core.Common;
using Vue.Core.Data;
using Vue.Core.Data.Entities;
using Vue.Core.Common.Config;
using Vue.Core.Model;
using Vue.Core.Dal;
using Vue.Core.Service;
using Vue.Core.Service.Interface;
using Vue.Core.helps;

namespace Vue.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private IUsersService<Users> _userService;
        public UsersController(IDistributedCache distributedCache,ApplicationDbContext db,IUsersService<Users> userService,
            IMapper mapper,IOptions<JwtSetting> jwtsetting) 
            :base(distributedCache,db,mapper,jwtsetting)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginModel data)
        {
            var user = _userService.Authenticate(data.Loginname, data.Password);

            if (user == null)
                return Unauthorized();
            
            var tokenString = TokenMan.GenToken(user,_jwtsetting,_jwtsetting.Expire);
            var refreshTokenString = TokenMan.GenToken(user,_jwtsetting,_jwtsetting.LongExpire);
            
            return await Task.FromResult(Ok(new {
                access_token = tokenString,
                refresh_token = refreshTokenString
            }));
        }

     

        [HttpGet]
        [Authorize(Policy = "ApiRead")]
        [Route("[action]")]
        public async Task<IEnumerable<UsersList>> GetAll()
        {
            return await Task.FromResult(_mapper.Map<List<UsersList>>(_userService.GetAll().ToList()));;
        }
    }
}