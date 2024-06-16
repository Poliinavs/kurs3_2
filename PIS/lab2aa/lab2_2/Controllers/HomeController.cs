using Microsoft.AspNetCore.Mvc;

namespace lab2_2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("https://localhost:7227/Index.html");
        }
    }
}
