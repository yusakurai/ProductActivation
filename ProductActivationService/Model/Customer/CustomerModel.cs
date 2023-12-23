using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductActivationService.Model
{
  [SwaggerSchema(Title = "Customer")]
  public class CustomerModel
  {
    /// <summary>
    /// 主キー
    /// </summary>
    /// <example>100</example>
    [JsonPropertyName("id")]
    public long Id { get; set; }
    /// <summary>
    /// 顧客名
    /// </summary>
    /// <example>顧客０１</example>
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
  }
}
