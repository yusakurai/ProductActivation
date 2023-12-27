using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductActivationService.Models
{
    /// <summary>
    /// 管理ログイン ポストモデル
    /// </summary>
    public class AdminLoginPostModel
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <example>admin</example>
        [Required(ErrorMessage = "idを設定してください")]
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// パスワード
        /// </summary>
        /// <example>admin123</example>
        [Required(ErrorMessage = "passwordを設定してください")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }
}
