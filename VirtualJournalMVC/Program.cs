using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.AspNetCore.Identity;
using Models;
using Service;
using IService;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("VirtualJournalConnection")
    ));


// Add services to the container.
builder.Services.AddControllersWithViews();


// Identity
builder.Services.AddDefaultIdentity<ApplicationUser>
    (options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUserService, UserManagerService>();


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

//Identity
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
