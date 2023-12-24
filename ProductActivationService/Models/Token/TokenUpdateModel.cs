using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductActivationService.Models
{
    /// <summary>
    /// トークン更新用モデル
    /// </summary>
    public class TokenUpdateModel
    {
        /// <summary>
        /// 顧客ID
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "顧客IDを設定してください")]
        [JsonPropertyName("customerId")]
        public long CustomerId { get; set; }

        /// <summary>
        /// クライアントGUID
        /// </summary>
        /// <example>a81bc81b-dead-4e5d-abff-90865d1e13b1</example>
        [Required(ErrorMessage = "clientGuidを設定してください")]
        [StringLength(32, ErrorMessage = "clientGuidは32文字以内で設定してください")]
        [JsonPropertyName("clientGuid")]
        public string ClientGuid { get; set; } = null!;

        /// <summary>
        /// クライアントホスト名
        /// </summary>
        /// <example>hostname</example>
        [JsonPropertyName("clientHostName")]
        public string ClientHostName { get; set; } = null!;
    }
}
