using Microsoft.AspNetCore.Mvc;

namespace DrinksNPicsAdmin.Controllers
{
    public class MovieManController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}