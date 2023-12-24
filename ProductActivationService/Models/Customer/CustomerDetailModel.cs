using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductActivationService.Models
{
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
        /// 名称
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
