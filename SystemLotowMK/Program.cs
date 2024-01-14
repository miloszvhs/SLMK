using FluentNHibernate;
using Hangfire;
using Hangfire.MemoryStorage;
using NHibernate.NetCore;
using NHibernate.AspNetCore.Identity;
using NHibernate.Cfg;
using ORM;
using ORM.Contract.Interfaces;
using Serilog;
using Serilog.Events;
using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;
using NHIdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

builder.Services.AddEnabledModules();

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

var cfg = new Configuration();
var file = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory + "\\resources",
    "hibernate.config");
cfg.Configure(file);
cfg.AddIdentityMappings();

var assemblies = AppDomain.CurrentDomain.GetAssemblies();
var specificAssemblies = assemblies.Where(x => x.GetTypes().Any(y => typeof(IMappingFromAssembly).IsAssignableFrom(y)));
                
foreach (var assembly in specificAssemblies)
    cfg.AddMappingsFromAssembly(assembly);

builder.Services.AddHibernate(cfg);
builder.Services.AddIdentity<NHIdentityUser, NHIdentityRole>()
    .AddHibernateStores();

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

app.Services.AddConnectionStrings();

app.Services.GetService<ModuleConfigurationService>().ConfigureModules();


app.Run();