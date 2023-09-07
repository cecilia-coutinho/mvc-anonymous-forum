using Microsoft.EntityFrameworkCore;
using AnonymousForum.Data;
using AnonymousForum.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AnonymousForumContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AnonymousForumContext") ?? throw new InvalidOperationException("Connection string 'AnonymousForumContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    DbHelper.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "account",
    pattern: "Account/{action=Login}/{id?}",
    defaults: new { controller = "Account" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "thread",
        pattern: "Threads/TopicThreads/{id}",
        defaults: new { controller = "Threads", action = "TopicThreads" });

app.Run();
