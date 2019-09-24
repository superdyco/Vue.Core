using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Vue.Core.Dal;
using Vue.Core.Data;

namespace Vue.Core.helps
{
    public class ApiAuthorizationHandler:AuthorizationHandler<ApiRequirement>
    {
        ApplicationDbContext _db;
        public ApiAuthorizationHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiRequirement requirement)
        {
            var dal = new UsersDal(_db);
            var identity = (ClaimsIdentity)context.User.Identity;

            if (!(context.Resource is RouteEndpoint endpoint)) {context.Fail();return Task.CompletedTask;}
            var path = endpoint.RoutePattern.RawText;
            if (identity != null)
            {
                var idObj = (identity.FindFirst("ID"));
                if (idObj== null || string.IsNullOrWhiteSpace(idObj.Value)){
                    context.Fail();
                }
                else
                {
                    if (dal.checkApiAuth(Convert.ToInt32(idObj.Value),path,requirement.Policy))
                        context.Succeed(requirement);
                    else
                        context.Fail();
                }
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
    
    public class ApiRequirement : IAuthorizationRequirement
    {
        public ApiRequirement(Enums.ApiPolicy policy)
        {
            Policy = policy;
        }
        public Enums.ApiPolicy Policy { get; set; }
    }
}