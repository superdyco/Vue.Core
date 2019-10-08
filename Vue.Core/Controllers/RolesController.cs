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
    public class RolesController : BaseController
    {
        private IRolesService<Roles> _RolesService;
     
        public RolesController(IDistributedCache distributedCache, ApplicationDbContext db,
            IRolesService<Roles> rolesService,IMapper mapper, IOptions<JwtSetting> jwtsetting,IHttpContextAccessor httpContextAccessor)
            : base(distributedCache, db, mapper, jwtsetting,httpContextAccessor)
        {
            _RolesService = rolesService;
        }

      
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> GetJson()
        {
            var model = _RolesService.GetJson();
            return await Task.FromResult(Ok(new
                {
                    items = _mapper.Map<List<RolesJsonModel>>(model.ToList())
                }
            ));
        }
     
    }
}