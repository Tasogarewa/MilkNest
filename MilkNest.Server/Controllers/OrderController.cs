using Microsoft.AspNetCore.Mvc;

namespace MilkNest.Server.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
