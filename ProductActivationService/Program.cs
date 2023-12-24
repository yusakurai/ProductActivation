using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Mappers;
using ProductActivationService.Repositories;
using ProductActivationService.Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ProductActivationService.Utils.Swagger;

var builder = WebApplication.CreateBuilder(args);

// DB接続
builder.Services.AddDbContext<MainContext>(opt =>
{
  var connection = builder.Configuration.GetConnectionString("MSSQLConnection");
  opt.UseSqlServer(connection);
});

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
// API バージョニング追加
builder.Services.AddApiVersioning(opt =>
{
  // クライアントにApiバージョンを通知
  opt.ReportApiVersions = true;
  opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                  new HeaderApiVersionReader("x-api-version"),
                                                  new MediaTypeApiVersionReader("x-api-version"));
});
// Swaggerオプション設定追加
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
// API バージョニング追加
// builder.Services.AddVersionedApiExplorer();
builder.Services.AddVersionedApiExplorer(setup =>
{
  // VVV: Major, optional minor version, and status
  // https://github.com/dotnet/aspnet-api-versioning/wiki/Version-Format#custom-api-version-format-strings
  setup.GroupNameFormat = "'v'VVV";
  // フォーマット方法を制御できるようにする
  setup.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
      options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
  });
}

// 強制的に HTTP 要求を HTTPS へリダイレクトする
app.UseHttpsRedirection();
// コントローラーで定義したアクションをエンドポイントに追加する
app.MapControllers();
app.Run();

// DIコンテナにMapperを追加する
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

// DIコンテナにリポジトリーを追加する
static void AddRepository(IServiceCollection service)
{
  // カスタマー
  // service.AddScoped<ICustomerRepository, CustomerRepository>();
  service.AddScoped<ICustomerRepository, CustomerRepositoryFake>();
}

// DIコンテナにサービスを追加する
static void AddService(IServiceCollection service)
{
  // カスタマー
  service.AddScoped<ICustomerService, CustomerService>();
}