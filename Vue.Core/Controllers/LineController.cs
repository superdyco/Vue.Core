using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
    public class LineController : BaseController
    {
        private ILineService _LineService;
        
        public LineController(IDistributedCache distributedCache, ApplicationDbContext db,
            ILineService LineService, 
            IMapper mapper, IOptions<JwtSetting> jwtsetting,IHttpContextAccessor httpContextAccessor)
            : base(distributedCache, db, mapper, jwtsetting,httpContextAccessor)
        {
            _LineService = LineService;
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post()
        {
            var token = isRock.LineBot.Utility.IssueChannelAccessToken("1653395375", "ee500885dc59dbea41eff99f1cbd989f");
            try
            {
                //LineBot Need Setting AllowSynchronousIO
                var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (syncIOFeature != null)
                {
                    syncIOFeature.AllowSynchronousIO = true;
                }
                
                //取得 http Post RawData(should be JSON)
                string postData = new StreamReader(Request.Body).ReadToEnd();
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                
                //回覆訊息
                string Message;
                switch (ReceivedMessage.events[0].message.text)
                {
                    case "news": Message = string.Join(",", _LineService.GetNews());
                        break;
                    default:
                        Message = "你說了:" + ReceivedMessage.events[0].message.text;
                        break;
                }
                //回覆用戶
                isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, token.access_token);
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
       
        
    }
}