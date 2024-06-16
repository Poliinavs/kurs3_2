using lab1a;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello");


app.MapGet("/APV", (HttpContext context) => {
    string parmA = context.Request.Query["ParmA"];
    string parmB = context.Request.Query["ParmB"];
    string responseText = $"GET-Http-APV:ParmA = {parmA},ParmB = {parmB}";
    context.Response.ContentType = "text/plain";
    return context.Response.WriteAsync(responseText);
});

app.MapPost("/APV", (HttpContext context) => {
    string parmA = context.Request.Form["ParmA"];
    string parmB = context.Request.Form["ParmB"];
    string responseText = $"POST-Http-APV:ParmA = {parmA},ParmB = {parmB}";
    context.Response.ContentType = "text/plain";
    return context.Response.WriteAsync(responseText);
});

app.MapPut("/APV", (HttpContext context) => {
    string parmA = context.Request.Query["ParmA"];
    string parmB = context.Request.Query["ParmB"];
    string responseText = $"PUT-Http-APV:ParmA = {parmA},ParmB = {parmB}";
    context.Response.ContentType = "text/plain";
    return context.Response.WriteAsync(responseText);
});

app.MapPost("/add", async (HttpContext context) =>
{
    int x = int.Parse(context.Request.Query["ParmX"]);
    int y = int.Parse(context.Request.Query["ParmY"]);
    int sum = x + y;

    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync(sum.ToString());
});

app.MapGet("/calcul", (HttpContext context) =>
{
    context.Response.ContentType = "text/html";
    return context.Response.WriteAsync(HtmlPage.GetHtmlPage());
});

app.MapGet("/calculXMLHTTP", (HttpContext context) =>
{
    context.Response.ContentType = "text/html";
    return context.Response.WriteAsync(HtmlPage.GetHtmlPage1());
});

app.MapPost("/multiply", (HttpContext context) =>
{
    context.Response.ContentType = "text/plain";
    try
    {
        int x = int.Parse(context.Request.Form["x"]);
        int y = int.Parse(context.Request.Form["y"]);
        int product = x * y;
        return context.Response.WriteAsync(product.ToString());
    }
    catch (Exception ex)
    {
        return context.Response.WriteAsync(ex.Message);
    }
});


app.Run();

