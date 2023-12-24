using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProductActivationService.Entities
{
    /// <summary>
    /// 顧客 エンティティ
    /// </summary>
    [Table("Customer")]
    [Comment("顧客")]
    public class CustomerEntity : BaseEntity
    {
        [Key]
        [Required]
        [Comment("ID")]
        public long Id { get; set; }

        [Required]
        [StringLength(40)]
        [Comment("顧客名")]
        public string? Name { get; set; }

        [Required]
        [StringLength(16)]
        [Comment("プロダクトキー")]
        public string? ProductKey { get; set; }

        [Comment("ライセンス数")]
        public int LicenseLimit { get; set; }
    }
}
