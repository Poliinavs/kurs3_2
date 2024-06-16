using Microsoft.AspNetCore.Mvc;

namespace lab2_3.Controllers
{
    public class StartController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult One()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Two()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Three()
        {
            return View();
        }
    }
}
