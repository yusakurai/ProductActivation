using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductActivationService.Models
{
    /// <summary>
    /// アクティベーション ポストモデル
    /// </summary>
    public class ActivationPostModel
    {
        /// <summary>
        /// 顧客ID
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "顧客IDを設定してください")]
        [JsonPropertyName("customerId")]
        public long CustomerId { get; set; }

        /// <summary>
        /// プロダクトキー
        /// </summary>
        /// <example>XXXXXXXXXXXXXXXX</example>
        [Required(ErrorMessage = "productKeyを設定してください")]
        [StringLength(16, ErrorMessage = "productKeyは16文字以内で設定してください")]
        [JsonPropertyName("productKey")]
        public string ProductKey { get; set; } = null!;

        /// <summary>
        /// クライアントGUID
        /// </summary>
        /// <example>a81bc81b-dead-4e5d-abff-90865d1e13b1</example>
        [Required(ErrorMessage = "clientGuidを設定してください")]
        [StringLength(36, ErrorMessage = "clientGuidは36文字以内で設定してください")]
        [JsonPropertyName("clientGuid")]
        public string ClientGuid { get; set; } = null!;

        /// <summary>
        /// クライアントホスト名
        /// </summary>
        /// <example>hostname</example>
        [Required(ErrorMessage = "clientHostNameを設定してください")]
        [JsonPropertyName("clientHostName")]
        public string ClientHostName { get; set; } = null!;
    }
}
