using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductActivationService.Models
{
    public class CustomerUpdateModel
    {
        /// <summary>
        /// 顧客名
        /// </summary>
        /// <example>顧客０１</example>
        [Required(ErrorMessage = "nameを設定してください")]
        [StringLength(20, ErrorMessage = "nameは20文字以内で設定してください")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// プロダクトキー
        /// </summary>
        /// <example>XXXXXXXXXXXXXXXX</example>
        [Required(ErrorMessage = "productKeyを設定してください")]
        [StringLength(16, ErrorMessage = "productKeyは16文字以内で設定してください")]
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
