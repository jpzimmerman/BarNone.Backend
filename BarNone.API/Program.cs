using BarNone.BusinessLogic.Services;
using BarNone.DataLayer;
using MySqlConnector;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

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
app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod().
      AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
