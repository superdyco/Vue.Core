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
    public class FakeTokenController : BaseController
    {
        private IUsersService<Users> _userService;
        private IUsersTokenService _usersTokenService;
        public FakeTokenController(IDistributedCache distributedCache,ApplicationDbContext db,
            IUsersService<Users> userService,IUsersTokenService usersTokenService,
            IMapper mapper,IOptions<JwtSetting> jwtsetting,IHttpContextAccessor httpContextAccessor) 
            :base(distributedCache,db,mapper,jwtsetting,httpContextAccessor)
        {
            _userService = userService;
            _usersTokenService = usersTokenService;
        }
       
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public  IActionResult Test()
        {
            return new EmptyResult();
        }
    }
}