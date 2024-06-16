var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // добавляем сервисы MVC

var app = builder.Build();

app.UseStaticFiles();

app.Run();

//https://localhost:7227/index.html