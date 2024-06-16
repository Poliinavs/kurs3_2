using Microsoft.AspNetCore.Mvc;
using System;

namespace lab4_2.Controllers
{
    public class StatusController : Controller
    {
        [HttpGet]
        public IActionResult S200()
        {
            Random random = new Random();
            int randomNumber = random.Next(200, 300);
            return StatusCode(randomNumber);
        }
        [HttpGet]
        public IActionResult S300()
        {
            Random random = new Random();
            int randomNumber = random.Next(300, 400);
            return StatusCode(randomNumber);
        }
        [HttpGet]
        public IActionResult S500()
        {
            Random random = new Random();
            int randomNumber = random.Next(500, 600);
            return StatusCode(randomNumber);
        }

    }
}
