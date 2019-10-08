using System;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Vue.Core.Data;
using Vue.Core.Common.Config;

namespace Vue.Core.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public IHttpContextAccessor _httpContextAccessor { get; }
        protected readonly IDistributedCache _distributedCache;
        protected ApplicationDbContext _db;
        protected JwtSetting _jwtsetting;
        protected IMapper _mapper;
        protected BaseController(IDistributedCache distributedCache,ApplicationDbContext db,IMapper mapper,IOptions<JwtSetting> jwtsetting
            ,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _distributedCache = distributedCache;
            _db = db;
            _jwtsetting = jwtsetting.Value;
            _mapper = mapper;
        }

        /// <summary>
        /// 拿取登錄的JWT Claim裡的使用者ID
        /// </summary>
        /// <returns></returns>
        protected int GetLoginId()
        {
            int result = default;
            if (_httpContextAccessor.HttpContext != null &&
                _httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                int.TryParse(identity.FindFirst("ID")?.Value, out result);;
            }

            return result;
        }
    }
}