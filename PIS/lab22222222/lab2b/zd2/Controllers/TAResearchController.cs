using Microsoft.AspNetCore.Mvc;

namespace ASPVMVC07.Controllers;

[Route("it")]
public class TAResearchController : Controller
{
    [HttpGet("{n:int}/{str}")]
    public IActionResult M04(int? n, string? str)
    {
        try
        {
            if (n is null) throw new("N is not integer");
            if (str is null) throw new("Str is not string");
            return Ok($"GET:M04:/{n}/{str}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AcceptVerbs("POST", "GET"), Route("{b:bool}/{letters::letters}")]
    public IActionResult M05(bool? b, string? letters)
    {
        try
        {
            if (b is null) throw new("B is not boolean");
            if (letters is null) throw new("Letters is not string");

            return Ok($"{HttpContext.Request.Method}:M05: /{b}/{letters}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AcceptVerbs("DELETE", "GET"), Route("{f:float}/{str::length(2, 5)}")]
    public IActionResult M06(float? f, string? str)
    {
        try
        {
            if (f is null) throw new("f is not float");
            if (str is null) throw new("str is not string");

            return Ok($"{HttpContext.Request.Method}:M06 /{f}/{str}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{letters::letters}/{n::range(100, 200)}")]
    public IActionResult M07(string? letters, int? n)
    {
        try
        {
            if (letters is null) throw new("Letters is not string");
            if (n is null) throw new("N is not integer");
            return Ok($"PUT:M07 /{letters}/{n}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{mail::email}")]
    public IActionResult M08(string? mail)
    {
        try
        {
            if (mail is null) throw new("Mail is not string");
            return Ok($"POST:M08 /{mail}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}