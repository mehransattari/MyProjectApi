using Common.Settings;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using WebFramework.Configuration;
using WebFramework.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

builder.Host.UseSerilog();

//Set SiteSettings and Use By IOptionsSnapshot<SiteSettings> settings in jwtService
builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

var siteSettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>()!;

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //AuthorizeFilter 
    options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddCustomIdentity(siteSettings.IdentitySettings);

builder.Services.AddScopedServices();

builder.Services.AddAttributeServices();

builder.Services.AddJwtService(siteSettings.JwtSettings);

var app = builder.Build();

app.UseCustomExceptionHandler();
//app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

