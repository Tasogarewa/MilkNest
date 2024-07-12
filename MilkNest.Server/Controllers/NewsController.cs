using Microsoft.AspNetCore.Mvc;

namespace MilkNest.Server.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
