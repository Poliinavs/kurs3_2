using Microsoft.AspNetCore.Mvc.Filters;

namespace lab3b_vd.Attributes;

public class ControllerActionAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // get query parameters _action and _controller
        var query = context.HttpContext.Request.Query;
        query.TryGetValue("_action", out var action);
        query.TryGetValue("_controller", out var controller);

        if (action.FirstOrDefault() is null || controller.FirstOrDefault() is null)
        {
            base.OnActionExecuting(context);
            return;
        }

        context.HttpContext.Response.Redirect($"/{controller}/{action}");
    }
}
