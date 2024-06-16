using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lab3b_vd.Attributes;

public class NonAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
            return;
        }
        base.OnActionExecuting(context);
    }
}
