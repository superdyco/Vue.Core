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
    public class NewsController : BaseController
    {
        private INewsService<News> _newsService;
        
        public NewsController(IDistributedCache distributedCache, ApplicationDbContext db,
            INewsService<News> newsService, 
            IMapper mapper, IOptions<JwtSetting> jwtsetting,IHttpContextAccessor httpContextAccessor)
            : base(distributedCache, db, mapper, jwtsetting,httpContextAccessor)
        {
            _newsService = newsService;
        }


        [HttpPost]
        [Authorize(Policy = "ApiRead")]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(Service.Fitlers.NewsFilter data)
        {
            var model = _newsService.GetAll(data);
            return await Task.FromResult(Ok(new
                {
                    items = _mapper.Map<List<NewsList>>(model.items.ToList()),
                    recordcount = model.recordcount
                }
            ));
        }
        
        [HttpGet]
        [Authorize(Policy = "ApiRead")]
        [Route("[action]")]
        public async Task<IActionResult> GetOne(Guid gid)
        {
            var model = _newsService.GetByGId(gid);
            return await Task.FromResult(Ok(model));
        }
        
        [HttpPost]
        [Authorize(Policy = "ApiWrite")]
        [Route("[action]")]
        public async Task<IActionResult> Create(News data)
        {
            var u=_newsService.Create(data);
           
            return await Task.FromResult(Ok());
        }
        
        [HttpPost]
        [Authorize(Policy = "ApiWrite")]
        [Route("[action]")]
        public async Task<IActionResult> Update(News data)
        {
            _newsService.Update(data);
           
            return await Task.FromResult(Ok());
        }
        
        
    }
}