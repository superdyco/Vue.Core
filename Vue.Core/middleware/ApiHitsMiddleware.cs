using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Vue.Core.Data;

namespace Vue.Core.middleware
{
    public class ApiHitsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        public ApiHitsMiddleware(RequestDelegate next,IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<ApplicationDbContext>();
                string path = context.Request.Path;
                var data = db.AppsApiCollection.FirstOrDefault(x => x.RoutePath == path);
                if (data != null)
                {
                    data.Hits += 1;
                    await db.SaveChangesAsync();
                }
            }

            await _next(context);
        }
        
        
    }
}