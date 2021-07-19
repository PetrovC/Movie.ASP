using Microsoft.AspNetCore.Mvc;

namespace MovieProject.ASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
