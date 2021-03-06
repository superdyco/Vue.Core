using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Vue.Core.Controllers;
using Vue.Core.Data;

namespace Vue.Core.Tests
{
    public class AppTest
    {
        private ApplicationDbContext db;
        private TestServer server;
        private HttpClient client;
        private string cookies;

        [SetUp]
        [OneTimeSetUp]
        public void Setup()
        {
            var builder = WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>() //the Startup can be MyWebApp.Startup if you have nothing to customize
                .ConfigureAppConfiguration(b => b.AddJsonFile("appsettings.json"));
            //Get Testing Setting
            builder.UseEnvironment("Testing");
            //呼叫server,會初始化server的startup.cs,並建立db
            server = new TestServer(builder);
            //給予Dbcontent
            db = server.Services.GetService<ApplicationDbContext>();
            //塞Seed
            client = server.CreateClient();
        }

        [TearDown]
        public void Cleanup()
        {
            //remove testing db
            //db.Database.EnsureDeleted();
        }

        [Test]
        [TestCase("admin", "p12345")]
        [Order(1)]
        public async Task TestLogin(string Loginname, string Password)
        {
            string url = "/api/Users/Login/";
            var pdata = new {Loginname = Loginname, Password = Password};
            var data = JsonConvert.SerializeObject(pdata);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            if (response.EnsureSuccessStatusCode().StatusCode == HttpStatusCode.OK)
            {
               
                IEnumerable<string> keys = null;
                var jwtToken = response.Headers.Where(x=>x.Key=="Set-Cookie").Select(x=>x.Value).FirstOrDefault();
                if (jwtToken != null)
                    cookies = jwtToken.FirstOrDefault();
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(true, response.EnsureSuccessStatusCode().StatusCode.ToString());
            }
        }

        [Test]
        [Order(2)]
        public async Task TestGetUsers()
        {
            var data = JsonConvert.SerializeObject(new Service.Fitlers.UsersFilter()
            {
                currentPage = 1, pageSize = 10, sortBy = new string[] {"CreatedAt"},
                sortDesc = new bool[] {true},
            });
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            content.Headers.Add("Cookie",cookies);
            var response = await client.PostAsync("/api/Users/GetAll/", content);
            if (response.EnsureSuccessStatusCode().StatusCode == HttpStatusCode.OK)
            {
                Assert.IsTrue(true);
            }
            else                   
            {
                Assert.IsFalse(true, response.EnsureSuccessStatusCode().StatusCode.ToString());
            }
        }
    }
}