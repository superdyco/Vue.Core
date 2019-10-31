using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Common;
using NLog.Web;
using LogLevel = NLog.LogLevel;

namespace Vue.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
              try
              {
                  InternalLogger.LogFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(),"logs",  "nlog-internals.log");
                  InternalLogger.LogLevel = LogLevel.Info;
                  CreateHostBuilder(args).Build().Run();
              }
              catch (Exception ex)
              {
                  InternalLogger.Log(LogLevel.Error, ex.Message);
                  throw;
              }
              finally
              {
                  // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                  NLog.LogManager.Shutdown();
              }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    
                    webBuilder.UseStartup<Startup>();
                });
    }
}