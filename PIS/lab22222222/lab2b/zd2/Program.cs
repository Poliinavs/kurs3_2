using ASPVMVC07.Constrains;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("letters", typeof(LettersRouteConstraint));
    options.ConstraintMap.Add("email", typeof(EmailRouteConstraint));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TAResearch}/{action=/it/1/2fds}/{id?}"
);

app.Run();