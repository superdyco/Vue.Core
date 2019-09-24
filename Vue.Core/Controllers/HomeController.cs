using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Vue.Core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}