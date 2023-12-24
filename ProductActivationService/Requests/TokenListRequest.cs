using Microsoft.AspNetCore.Mvc;

namespace ProductActivationService.Requests
{
    /// <summary>
    /// トークン一覧リクエスト
    /// </summary>
    public class TokenListRequest
    {
        /// <summary>
        /// sub
        /// </summary>
        [FromQuery(Name = "sub")]
        public string? Sub { get; set; }
    }
}
