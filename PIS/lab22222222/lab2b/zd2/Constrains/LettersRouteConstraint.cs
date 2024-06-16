
using System.Text.RegularExpressions;

namespace ASPVMVC07.Constrains;

public class LettersRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        var regex = new Regex(@"^[a-zA-Zа-яА-Я]+$");
        return values[routeKey] is not null && regex.IsMatch(values[routeKey].ToString());
    }
}