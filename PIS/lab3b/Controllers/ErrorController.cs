using Microsoft.AspNetCore.Mvc;

namespace lab3b.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Admin/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                    break;
            }
            return View("Error");
        }
    }
}
