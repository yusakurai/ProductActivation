using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Mappers;
using ProductActivationService.Repositories;
using ProductActivationService.Services;

var builder = WebApplication.CreateBuilder(args);

// DB接続
builder.Services.AddDbContext<MainContext>(opt =>
{
  var connection = builder.Configuration.GetConnectionString("MSSQLConnection");
  opt.UseSqlServer(connection);
});

// DIコンテナにサービスを追加する
AddService(builder.Services);

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

// 強制的に HTTP 要求を HTTPS へリダイレクトする
app.UseHttpsRedirection();
// コントローラーで定義したアクションをエンドポイントに追加する
app.MapControllers();
app.Run();

/// <summary>
/// DIコンテナにサービスを追加する
/// </summary>
/// <param name="service"></param>
static void AddService(IServiceCollection service)
{
  // AutoMapper登録
  service.AddAutoMapper(config =>
  {
    AutoMapperConfig.AddProfile(config);
  });
  // MapperをSingletonにする
  service.AddSingleton<IMapper, Mapper>();
  // コントローラー追加
  service.AddControllers();
  // Repository登録
  // service.AddScoped<ICustomerRepository, CustomerRepository>();
  service.AddScoped<ICustomerRepository, CustomerRepositoryFake>();
  // Service登録
  service.AddScoped<ICustomerService, CustomerService>();


}