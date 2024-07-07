using Common.Settings;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using System.Configuration;
using WebFramework.Configuration;
using WebFramework.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //AuthorizeFilter 
    options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddServices();

builder.Services.AddAttributeServices();

//Set SiteSettings and Use By IOptionsSnapshot<SiteSettings> settings in jwtService
builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

var siteSettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>()!;

builder.Services.AddJwtService(siteSettings.JwtSettings);



//builder.Services.AddCustomIdentity(siteSettings.IdentitySettings);

var app = builder.Build();

app.UseCustomExceptionHandler();
//app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

