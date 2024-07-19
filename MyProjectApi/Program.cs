using Common.Settings;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using WebFramework.Configuration;
using WebFramework.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

builder.Host.UseSerilog();

var Services = builder.Services;

//Set SiteSettings and Use By IOptionsSnapshot<SiteSettings> settings in jwtService
Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

var siteSettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>()!;

// Add services to the container.

Services.AddControllers(options =>
{
    //AuthorizeFilter 
    options.Filters.Add(new AuthorizeFilter());
});

Services.AddMongoServices(builder.Configuration);

Services.AddDbContext(builder.Configuration);

Services.AddCustomIdentity(siteSettings.IdentitySettings);

Services.AddScopedServices();

Services.AddAttributeServices();

Services.AddJwtService(siteSettings.JwtSettings);

var app = builder.Build();

app.UseCustomExceptionHandler();

//app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

