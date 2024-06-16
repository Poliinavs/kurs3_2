using Microsoft.AspNetCore.Mvc;

namespace lab2b.Controllers;

public class TResearchController : Controller
{
    [HttpGet, Route("/MView")]
    public IActionResult MView()
    {
        return View();
    }

    [
        HttpGet,
        Route("/MResearch/M01/1"),
        Route("/MResearch/M01"),
        Route("/MResearch"),
        Route("/"),
        Route("/V2/MResearch/M01"),
        Route("/V3/MResearch/{str}/M01")
    ]
    public IActionResult M01(string? str)
    {
        return Ok($"GET:M01 {(str is not null ? $"| string - {str}" : "")}");
    }

    [
        HttpGet,
        Route("/V2"),
        Route("/V2/MResearch"),
        Route("/V2/MResearch/M02"),
        Route("/MResearch/M02"),
        Route("/V3/MResearch/{str}/M02")
    ]
    public IActionResult M02(string? str)
    {
        return Ok($"GET:M02 {(str is not null ? $"| string - {str}" : "")}");
    }

    [
        HttpGet,
        Route("/V3"),
        Route("/V3/MResearch/{str}"),
        Route("/V3/MResearch/{str}/M03")
    ]
    public IActionResult M03(string? str)
    {
        return Ok($"GET:M03 {(str is not null ? $"| string - {str}" : "")}");
    }

    [HttpGet, Route("/{*any}")]
    public IActionResult MXX()
    {
        return Ok("GET:MXX");
    }
}