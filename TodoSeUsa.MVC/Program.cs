using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoSeUsaNet7.Models.Data;
var builder = WebApplication.CreateBuilder(args);

// Get the connection string from the environment variable
var connectionString = Environment.GetEnvironmentVariable("TODOSEUSANET7_CONNECTION_STRING");

builder.Services.AddDbContext<TodoSeUsaNet7Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TodoSeUsaNet7User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<TodoSeUsaNet7Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequiredLength = 12;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoSeUsaNet7Context>();
    // Applies any pending migrations
    dbContext.Database.Migrate();
}


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
