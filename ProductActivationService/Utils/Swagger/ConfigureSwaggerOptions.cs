using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace ProductActivationService.Utils.Swagger
{
  /// <summary>
  /// Swaggerオプション設定クラス
  /// </summary>
  public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
  {

    private IApiVersionDescriptionProvider Provider => provider;

    /// <summary>
    /// 各APIバージョンをSwagger用に設定する
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
      foreach (var description in Provider.ApiVersionDescriptions)
      {
        options.SwaggerDoc(
            description.GroupName,
            CreateVersionInfo(description)
        );
      }
    }

    /// <summary>
    /// Swaggerオプションの設定（インターフェイスからの継承）
    /// </summary>
    /// <param name="name"></param>
    /// <param name="options"></param>
    public void Configure(string? name, SwaggerGenOptions options)
    {
      Configure(options);
    }

    /// <summary>
    /// APIのバージョン情報を作成する
    /// </summary>
    /// <param name="desc"></param>
    /// <returns>Information about the API</returns>
    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription desc)
    {
      var info = new OpenApiInfo()
      {
        Title = "アクティベーションサービス API",
        Version = desc.ApiVersion.ToString()
      };

      if (desc.IsDeprecated)
      {
        info.Description += "🚧 このAPIバージョンは廃止されました。新しいバージョンを使用してください。";
      }

      return info;
    }
  }
}