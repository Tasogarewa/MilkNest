using Microsoft.AspNetCore.Mvc;

namespace MilkNest.Server.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
