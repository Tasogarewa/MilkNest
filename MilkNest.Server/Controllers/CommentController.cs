using Microsoft.AspNetCore.Mvc;

namespace MilkNest.Server.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
