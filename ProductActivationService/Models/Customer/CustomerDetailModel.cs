using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductActivationService.Models
{
    /// <summary>
    /// 顧客詳細モデル
    /// </summary>
    [SwaggerSchema(Title = "CustomerDetail")]
    public class CustomerDetailModel : TimestampModel
    {
        /// <summary>
        /// ID
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

        /// <summary>
        /// プロダクトキー
        /// </summary>
        /// <example>XXXXXXXXXXXXXXXX</example>
        [JsonPropertyName("productKey")]
        public string ProductKey { get; set; } = null!;

        /// <summary>
        /// ライセンス数
        /// </summary>
        /// <example>100</example>
        [JsonPropertyName("licenseLimit")]
        public int LicenseLimit { get; set; }
    }
}
