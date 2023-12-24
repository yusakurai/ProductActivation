using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProductActivationService.Entities
{
    /// <summary>
    /// トークン エンティティ
    /// </summary>
    [Table("Token")]
    [Comment("トークン")]
    public class TokenEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("sub")]
        public Guid Sub { get; set; }

        [Required]
        [Comment("顧客ID")]
        public long CustomerId { get; set; }

        [Required]
        [StringLength(36)]
        [Comment("クライアントGUID")]
        public string? ClientGuid { get; set; }

        [StringLength(40)]
        [Comment("クライアントホスト名")]
        public string? ClientHostName { get; set; }

        [Comment("有効期限")]
        public long Exp { get; set; }
    }
}
