using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoSeUsaNet7.Areas.Identity.Data;
using TodoSeUsaNet7.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TodoSeUsaNet7ContextConnection") ?? throw new InvalidOperationException("Connection string 'TodoSeUsaNet7ContextConnection' not found.");

builder.Services.AddDbContext<TodoSeUsaNet7Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TodoSeUsaNet7User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TodoSeUsaNet7Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
