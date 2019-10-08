using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
    public class NavController : BaseController
    {
        public NavController(IDistributedCache distributedCache, ApplicationDbContext db,
           IMapper mapper, IOptions<JwtSetting> jwtsetting,IHttpContextAccessor httpContextAccessor)
            : base(distributedCache, db, mapper, jwtsetting,httpContextAccessor)
        {
           
        }

      
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post()
        {
            BaseSysDal v=new BaseSysDal(_db);
            var model=v.GenNav(GetLoginId());
            JObject root = JsonConvert.DeserializeObject<JObject>(model);
            var jarray = root["items"] as JArray;
            var dict = jarray.Where(x => x["children"] == null && (x["routename"] == null || x["routename"].Value<string>() == "")).Select(x=>x);
            foreach (var rm in dict.ToList())
            {
                jarray.Remove(rm);
            }
            
            var dict1 = jarray.Where(x => x["children"] != null).Select(x=>x);
            foreach (var rm in dict1)
            {
                var jo = rm as JObject;
                jo.Add(new JProperty("icon-alt", "keyboard_arrow_down"));
            }

            string json = root.ToString(Newtonsoft.Json.Formatting.None);
            
            return await Task.FromResult(Content(json));
        }
     
    }
}