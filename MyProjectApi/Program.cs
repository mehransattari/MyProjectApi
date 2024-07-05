using Common.Settings;
using WebFramework.Configuration;
using WebFramework.Middlewares;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddServices();

builder.Services.AddAttributeServices();

var siteSettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>()!;

builder.Services.AddCustomIdentity(siteSettings.IdentitySettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
