using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductActivationService.Models
{
    /// <summary>
    /// トークンリストモデル
    /// </summary>
    [SwaggerSchema(Title = "TokenList")]
    public class TokenListModel
    {
        /// <summary>
        /// sub
        /// </summary>
        /// <example>a81bc81b-dead-4e5d-abff-90865d1e13b1</example>
        [JsonPropertyName("sub")]
        public string Sub { get; set; } = null!;

        /// <summary>
        /// 顧客ID
        /// </summary>
        /// <example>1</example>
        [JsonPropertyName("customerId")]
        public long CustomerId { get; set; }

        /// <summary>
        /// クライアントGUID
        /// </summary>
        /// <example>a81bc81b-dead-4e5d-abff-90865d1e13b1</example>
        [JsonPropertyName("clientGuid")]
        public string ClientGuid { get; set; } = null!;

        /// <summary>
        /// クライアントホスト名
        /// </summary>
        /// <example>hostname</example>
        [JsonPropertyName("clientHostName")]
        public string ClientHostName { get; set; } = null!;

        /// <summary>
        /// 有効期限（UNIX時間）
        /// </summary>
        /// <example>1703323478</example>
        [JsonPropertyName("exp")]
        public long Exp { get; set; }
    }
}
