using BarNone.BusinessLogic.Services;
using BarNone.DataLayer;
using MySqlConnector;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDbConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSingleton<IDataRepository, DataRepository>();
builder.Services.AddSingleton<MenuDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
