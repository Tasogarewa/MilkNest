using Microsoft.AspNetCore.Mvc;

namespace MilkNest.Server.Controllers
{
    public class JobVacancy : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
