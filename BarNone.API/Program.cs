using BarNone.API;
using BarNone.BusinessLogic.Services;
using BarNone.DataLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using MySqlConnector;
using System.Data;

var builder = WebApplication.CreateBuilder(args);



var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = configBuilder.Build();


// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.EnableAnnotations();
});
builder.Services.AddTransient<IDbConnection>(_ =>
    new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION")));
builder.Services.AddSingleton<IMenuDataRepository, MenuMsDataRepository>();
builder.Services.AddSingleton<BarInventoryMsDataRepository, BarInventoryMsDataRepository>();
builder.Services.AddSingleton<DataRepository, DataRepository>();
builder.Services.AddSingleton<MenuDataService>();
builder.Services.AddSingleton<InventoryDataService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie("Cookies", options =>
    {
        options.ExpireTimeSpan = System.TimeSpan.FromMinutes(Constants.LoginTtl);
        options.LoginPath = new PathString("/api/Auth/Login");
        options.LogoutPath = "/api/auth/Logout";
        options.ReturnUrlParameter = "api/menu";
        options.Cookie.Name = "mycookie"; options.Events.OnSigningOut = async e =>
        {
            await e.HttpContext.SignOutAsync();
        };
        options.Events = new CookieAuthenticationEvents
        {
        };
    })
    .AddGoogle("Google", googleOptions =>
    {
        googleOptions.ClientId = configuration.GetValue<string>("Google:ClientId") ?? string.Empty;
        googleOptions.ClientSecret = configuration.GetValue<string>("Google:ClientSecret") ?? string.Empty;
        googleOptions.Scope.Add("");
        googleOptions.SaveTokens = true;
        googleOptions.Events = new OAuthEvents
        {
            OnAccessDenied = async context =>
            {
                context.AccessDeniedPath = "/";
                context.ReturnUrl = "";
                context.ReturnUrlParameter = "/";
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("urn:barnone:type", "admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod().
      AllowAnyOrigin());

app.MapControllers();

app.Run();
