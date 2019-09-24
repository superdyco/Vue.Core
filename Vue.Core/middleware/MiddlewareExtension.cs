using System;
using Microsoft.AspNetCore.Builder;

namespace Vue.Core.middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseApiHitsMiddleware(this IApplicationBuilder app,IServiceProvider serviceProvider)
        {
            app.UseMiddleware<ApiHitsMiddleware>(serviceProvider);
            return app;
        }
    }
}