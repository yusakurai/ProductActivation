using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Mappers;
using ProductActivationService.Models;
using ProductActivationService.Repositories;
using ProductActivationService.Services;
using ProductActivationService.Utils.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DB接続
builder.Services.AddDbContext<MainContext>(opt =>
{
    var connection = builder.Configuration.GetConnectionString("MSSQLConnection");
    opt.UseSqlServer(connection);
});

// AppSetting
builder.Services.Configure<AppSettings>(builder.Configuration);

// マッパー追加
AddMapper(builder.Services);
// コントローラー追加
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    // SwaggerのBad Responseのサンプルモデルを非表示にする
    options.SuppressMapClientErrors = true;
});
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

// JWT認証追加
var jwtConfiguration = builder.Configuration.GetValue<string>("Jwt:Key");
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    // トークンの検証を行う
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = securityKey,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
    };
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
        // 顧客
        CustomerAutoMapperConfig.AddProfile(config);
        // トークン
        TokenAutoMapperConfig.AddProfile(config);
    });
    // MapperをSingletonにする
    service.AddSingleton<IMapper, Mapper>();
}

// DIコンテナにリポジトリーを追加する
static void AddRepository(IServiceCollection service)
{
    // 顧客
    service.AddScoped<ICustomerRepository, CustomerRepository>();
    // トークン
    service.AddScoped<ITokenRepository, TokenRepository>();
}

// DIコンテナにサービスを追加する
static void AddService(IServiceCollection service)
{
    // 顧客
    service.AddScoped<ICustomerService, CustomerService>();
    // トークン
    service.AddScoped<ITokenService, TokenService>();
    // アクティベーション
    service.AddScoped<IActivationService, ActivationService>();
    // 管理ログイン
    service.AddScoped<IAdminLoginService, AdminLoginService>();
}

// Testプロジェクトから参照できるようにする
public partial class Program { }
