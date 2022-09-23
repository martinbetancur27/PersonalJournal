using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Service;
using IService;
using Microsoft.AspNetCore.Identity.UI.Services;


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
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    })
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUserService, UserManagerService>();
builder.Services.AddScoped<IAuthorizeOwner, AuthorizeOwner>();


builder.Services.AddScoped<IJournal, JournalService>();
builder.Services.AddScoped<INote, NoteService>();
builder.Services.AddScoped<IComment, CommentService>();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Email

builder.Services.Configure<SmtpService>(options =>
{
    options.HostAddress = builder.Configuration.GetValue<string>("ExternalProviders:Gmail:SMTP:Address");
    options.HostPort = Convert.ToInt32(builder.Configuration.GetValue<string>("ExternalProviders:Gmail:SMTP:Port"));
    options.HostUsername = builder.Configuration.GetValue<string>("ExternalProviders:Gmail:SMTP:Account");
    options.HostPassword = builder.Configuration.GetValue<string>("ExternalProviders:Gmail:SMTP:Password");
    options.SenderEmail = builder.Configuration.GetValue<string>("ExternalProviders:Gmail:SMTP:SenderEmail");
    options.SenderName = builder.Configuration.GetValue<string>("ExternalProviders:Gmail:SMTP:SenderName");
});

builder.Services.AddTransient<IEmailSender, EmailServiceMailKit> ();


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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
