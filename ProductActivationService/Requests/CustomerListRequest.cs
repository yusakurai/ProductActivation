using Microsoft.AspNetCore.Mvc;

namespace ProductActivationService.Requests
{
    /// <summary>
    /// 顧客一覧リクエスト
    /// </summary>
    public class CustomerListRequest
    {
        /// <summary>
        /// 顧客名
        /// </summary>
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
