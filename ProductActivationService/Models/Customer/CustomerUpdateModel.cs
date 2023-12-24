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
        [StringLength(255, ErrorMessage = "nameは255文字未満で設定してください")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
}
