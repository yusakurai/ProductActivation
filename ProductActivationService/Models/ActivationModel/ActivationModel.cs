using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductActivationService.Models
{
    /// <summary>
    /// アクティベーションモデル
    /// </summary>
    [SwaggerSchema(Title = "Activation")]
    public class ActivationModel
    {
        /// <summary>
        /// アクセストークン
        /// </summary>
        /// <example>eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIn0.dw1oz77jAV2AUdIv7MAiarGl1EmVM8HGJUPxCaC5GUyD6VLp3c8K58fbgOPnslpgDf8wmgiwr2KzgPPNXCX5ebxz3b_q-09fHirXn_8fhUV2GAbcvgW9aCL8LxmUH-zLbyBYWcdc-GGFucOVNCB7uP-nWHgjim7BLiyUn1XwRuJhZTaZtMGnAgZ8oTw83yznLFdjpZBD9NUGE_m_FlGT_7559ixUk1jQVPkDjRZldQWwjSSzHVwLpQXHgCoHhWhRykmTgyg8KERtwywvBJikQABOEYw592uP2cWl023g3reZu8xl-17ojSFUplz2J4zqMhqZptP3z7kpe0C_SQQlNw</example>
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = null!;

        /// <summary>
        /// トークン種類
        /// </summary>
        /// <example>Bearer</example>
        [JsonPropertyName("tokenType")]
        public string TokenType { get; set; } = null!;

        /// <summary>
        /// 有効期限
        /// </summary>
        /// <example></example>
        [JsonPropertyName("exp")]
        public long Exp { get; set; }
    }
}
