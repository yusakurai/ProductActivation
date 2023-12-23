using Microsoft.EntityFrameworkCore;
using ProductActivation.Service.Data;

var builder = WebApplication.CreateBuilder(args);

// DB接続
builder.Services.AddDbContext<MainContext>(opt =>
{
  var connection = builder.Configuration.GetConnectionString("MSSQLConnection");
  opt.UseSqlServer(connection);
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

