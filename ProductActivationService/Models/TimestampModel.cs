using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductActivationService.Models
{
    /// <summary>
    /// タイムスタンプモデル
    /// </summary>
    public class TimestampModel
    {
        /// <summary>
        /// 登録日時
        /// </summary>
        /// <example>2023-12-24 12:14:21.9793310</example>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        /// <example>2023-12-24 12:14:21.9793310</example>
        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 削除日時
        /// </summary>
        /// <example>2023-12-24 12:14:21.9793310</example>
        [JsonPropertyName("deletedAt")]
        public DateTime? DeletedAt { get; set; }
    }
}
