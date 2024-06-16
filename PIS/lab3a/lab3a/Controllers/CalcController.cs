using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab3a.Controllers
{
    public class CalcController : Controller
    {
        public ActionResult Index(string? press, float? x, float? y)
        {
            float? z = 0;
            ViewBag.press = press;
            ViewBag.x = x;
            ViewBag.y = y;
            ViewBag.z = z;

            return View();
        }
        [HttpPost]
        public ActionResult Sum(string? press, float? x, float? y)
        {
            float? z = 0;
            try
            {
                if(press is null) throw new("str is not symbol '+'");
                if (x is null)
                {
                    x = 0;
                    throw new("x is not float");
                }
                if (y is null)
                {
                    y = 0;
                    throw new("y is not float");
                }

                ViewBag.z = x + y;
            }
            catch(Exception ex) {
                ViewBag.Error = ex.Message;
                ViewBag.z = z;
            }
            finally
            {
                ViewBag.press = press;
                ViewBag.x = x;
                ViewBag.y = y;
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Sub(string? press, float? x, float? y)
        {
            float? z = 0;
            try
            {
                if (press is null) throw new("str is not symbol '-'");
                if (x is null)
                {
                    x = 0;
                    throw new("x is not float");
                }
                if (y is null)
                {
                    y = 0;
                    throw new("y is not float");
                }

                ViewBag.z = x - y;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.z = z;
            }
            finally
            {
                ViewBag.press = press;
                ViewBag.x = x;
                ViewBag.y = y;
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Mul(string? press, float? x, float? y)
        {
            float? z = 0;
            try
            {
                if (press is null) throw new("str is not symbol '*'");
                if (x is null)
                {
                    x = 0;
                    throw new("x is not float");
                }
                if (y is null)
                {
                    y = 0;
                    throw new("y is not float");
                }

                ViewBag.z = x * y;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.z = z;
            }
            finally
            {
                ViewBag.press = press;
                ViewBag.x = x;
                ViewBag.y = y;
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Div(string? press, float? x, float? y)
        {
            float? z = 0;
            try
            {
                if (press is null) throw new("str is not symbol '/'");
                if (x is null)
                {
                    x = 0;
                    throw new("x is not float");
                }
                if (y is null)
                {
                    y = 0;
                    throw new("y is not float");
                }

                ViewBag.z = x / y;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.z = z;
            }
            finally
            {
                ViewBag.press = press;
                ViewBag.x = x;
                ViewBag.y = y;
            }
            return View("Index");
        }

       
    }
}
