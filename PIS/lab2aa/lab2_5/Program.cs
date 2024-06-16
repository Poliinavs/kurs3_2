var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(); // добавляем сервисы MVC

var app = builder.Build();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
