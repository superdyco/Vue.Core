using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Vue.Core.Data;
using Vue.Core.Common.Config;

namespace Vue.Core.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IDistributedCache _distributedCache;
        protected ApplicationDbContext _db;
        protected JwtSetting _jwtsetting;
        protected IMapper _mapper;
        protected BaseController(IDistributedCache distributedCache,ApplicationDbContext db,IMapper mapper,IOptions<JwtSetting> jwtsetting)
        {
            _distributedCache = distributedCache;
            _db = db;
            _jwtsetting = jwtsetting.Value;
            _mapper = mapper;
        }
    }
}