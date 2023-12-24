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
        [StringLength(32)]
        [Comment("クライアントGUID")]
        public string? ClientGuid { get; set; }

        [StringLength(40)]
        [Comment("クライアントホスト名")]
        public string? ClientHostName { get; set; }

        [Comment("JWT")]
        public string? Jwt { get; set; }

        [Comment("有効期限")]
        public long Exp { get; set; }
    }
}
