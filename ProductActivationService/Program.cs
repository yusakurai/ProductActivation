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

// API Versioning
builder.Services.AddApiVersioning();
// マッパー追加
AddMapper(builder.Services);
// コントローラー追加
builder.Services.AddControllers();
// リポジトリー追加
AddRepository(builder.Services);
// サービス追加
AddService(builder.Services);
// Swagger生成追加
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
/// DIコンテナにMapperを追加する
/// </summary>
/// <param name="service"></param>
static void AddMapper(IServiceCollection service)
{
  // AutoMapper登録
  service.AddAutoMapper(config =>
  {
    AutoMapperConfig.AddProfile(config);
  });
  // MapperをSingletonにする
  service.AddSingleton<IMapper, Mapper>();
}

/// <summary>
/// DIコンテナにリポジトリーを追加する
/// </summary>
/// <param name="service"></param>
static void AddRepository(IServiceCollection service)
{
  // カスタマー
  // service.AddScoped<ICustomerRepository, CustomerRepository>();
  service.AddScoped<ICustomerRepository, CustomerRepositoryFake>();
}

/// <summary>
/// DIコンテナにサービスを追加する
/// </summary>
/// <param name="service"></param>
static void AddService(IServiceCollection service)
{
  // カスタマー
  service.AddScoped<ICustomerService, CustomerService>();
}