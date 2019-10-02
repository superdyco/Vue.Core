using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vue.Core.Common;
using Vue.Core.Data;
using Vue.Core.Data.Entities;
using Vue.Core.Common.Config;
using Vue.Core.Model;
using Vue.Core.Dal;
using Vue.Core.Service.Interface;
using Vue.Core.helps;


namespace Vue.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private IUsersService<Users> _userService;
        private IUsersTokenService _usersTokenService;

        public UsersController(IDistributedCache distributedCache, ApplicationDbContext db,
            IUsersService<Users> userService, IUsersTokenService usersTokenService,
            IMapper mapper, IOptions<JwtSetting> jwtsetting)
            : base(distributedCache, db, mapper, jwtsetting)
        {
            _userService = userService;
            _usersTokenService = usersTokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginModel data)
        {
            var user = _userService.Authenticate(data.Loginname, data.Password);

            if (user == null)
                return Unauthorized();

            var getToken = TokenMan.GenToken(user, _jwtsetting, _jwtsetting.Expire);
            var getRefreshToken = TokenMan.GenToken(user, _jwtsetting, _jwtsetting.LongExpire);
            var result = _usersTokenService.SaveToken(user.Id, getToken.tokenString, getRefreshToken.tokenString,
                getToken.expireTo, getRefreshToken.expireTo);
            if (!result) return StatusCode(StatusCodes.Status500InternalServerError);

            return await Task.FromResult(Ok(new
            {
                access_token = getToken.tokenString,
                refresh_token = getRefreshToken.tokenString
            }));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel data)
        {
            var usersid = _usersTokenService.CheckRefreshToken(data.access_token, data.access_refreshtoken);
            if (usersid == 0)
                return Unauthorized();
            var user = _userService.GetById(usersid.Value);
            //如果存在,重新產生出jwtToken
            var getToken = TokenMan.GenToken(user, _jwtsetting, _jwtsetting.Expire);
            var getRefreshToken = TokenMan.GenToken(user, _jwtsetting, _jwtsetting.LongExpire);
            var result = _usersTokenService.UpdateToken(user.Id, getToken.tokenString, getRefreshToken.tokenString,
                getToken.expireTo, getRefreshToken.expireTo);
            if (!result) return StatusCode(StatusCodes.Status500InternalServerError);

            return await Task.FromResult(Ok(new
            {
                access_token = getToken.tokenString,
                refresh_token = getRefreshToken.tokenString
            }));
        }


        [HttpPost]
        [Authorize(Policy = "ApiRead")]
        [Route("[action]")]
        //[ProducesResponseType(typeof(PagingModel<List<UsersList>>), 200)]
        public async Task<IActionResult> GetAll(Service.Fitlers.UsersFilter data)
        {
            var model = _userService.GetAll(data);
            return await Task.FromResult(Ok(new
                {
                    items = _mapper.Map<List<UsersList>>(model.items.ToList()),
                    recordcount = model.recordcount
                }
            ));
        }
    }
}