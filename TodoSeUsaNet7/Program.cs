using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoSeUsaNet7.Models.Data;
using TodoSeUsaNet7.Models.Seeding;
using TodoSeUsaNet7.Models.Services;


var builder = WebApplication.CreateBuilder(args);
<<<<<<< HEAD
// Get the connection string from the environment variable
var connectionString = Environment.GetEnvironmentVariable("TODOSEUSANET7_CONNECTION_STRING_DEMO");
/*                       ?? Environment.GetEnvironmentVariable("TODOSEUSANET7_CONNECTION_STRING");
*/
=======

// Get the connection string from the environment variable
var connectionString = Environment.GetEnvironmentVariable("TODOSEUSANET7_CONNECTION_STRING");
>>>>>>> master

builder.Services.AddDbContext<TodoSeUsaNet7Context>(options => options.UseSqlServer(connectionString));

// DatabaseResetService Service
builder.Services.AddScoped<DatabaseResetService>();

builder.Services.AddHostedService<DatabaseResetHostedService>();

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
<<<<<<< HEAD

    // Applies any pending migrations
    dbContext.Database.Migrate(); 

    // Seed data
    await DataSeeder.SeedDataAsync(dbContext);
=======
    // Applies any pending migrations
    dbContext.Database.Migrate();
>>>>>>> master
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
