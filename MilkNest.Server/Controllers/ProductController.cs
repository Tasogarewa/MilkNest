using Microsoft.AspNetCore.Mvc;

namespace MilkNest.Server.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
