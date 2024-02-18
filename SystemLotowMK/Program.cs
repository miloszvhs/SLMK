using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Events;
using SystemLotowMK.Application;
using SystemLotowMK.Domain;
using SystemLotowMK.Domain.Entities;
using SystemLotowMK.Infrastructure;
using SystemLotowMK.Infrastructure.ApplicationContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidation();

builder.Services.AddDomainLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

// Add services to the container.
builder.Services.AddControllersWithViews();

//add identity user
builder.Services.AddIdentity<User, IdentityRole>(config =>
{
    config.Password.RequiredLength = 8;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    config.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Logging.AddSerilog(new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger());

builder.Services.AddHangfire(config =>
{
    config.UseSerilogLogProvider();
    config.UseMemoryStorage();
});

builder.Services.AddHangfireServer();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "User.Cookie";
        config.LoginPath = "/Home/Authenticate";
        config.LogoutPath = "/Home/Logout";
    });

builder.Services.AddAuthorization(config => 
{
    config.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim("Admin"));
    config.AddPolicy("User", policyBuilder => policyBuilder.RequireClaim("User"));
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHangfireDashboard();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
    
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();