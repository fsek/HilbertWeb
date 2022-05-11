using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HilbertWeb.BackendApp.Database;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authorization;
using HilbertWeb.BackendApp.Permission;
using HilbertWeb.BackendApp.Models.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
#region Logging

builder.Logging.SetMinimumLevel(LogLevel.Warning);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

#endregion

var connectionString = builder.Configuration.GetConnectionString("HilbertWebConnectionString");

#region Identity

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options => {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = new TimeSpan(0, 5, 0);
    });

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if(builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:3000").AllowCredentials().AllowAnyHeader().AllowAnyMethod()));
}

var app = builder.Build();

// for running behind reverse proxy
if (!app.Environment.IsDevelopment())
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

//httpsredirect applied on nginx level
//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

#region Seeding
#if DEBUG
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

    await HilbertWeb.BackendApp.Database.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
    await HilbertWeb.BackendApp.Database.Seeds.DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
    await HilbertWeb.BackendApp.Database.Seeds.DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
}
#endif
#endregion

app.Run();
