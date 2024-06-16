
using System.Text.RegularExpressions;

namespace ASPVMVC07.Constrains;

public class EmailRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return values[routeKey] is not null && regex.IsMatch(values[routeKey].ToString());
    }
}