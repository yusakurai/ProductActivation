using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProductActivationService.Entities
{
    public class BaseEntity
    {
        [Column("created_at")]
        [Comment("登録日時")]
        public DateTime? Created { get; set; }

        [Column("updated_at")]
        [Comment("更新日時")]
        public DateTime? Updated { get; set; }

        [Column("deleted_at")]
        [Comment("削除日時")]
        public DateTime? Deleted { get; set; }

        [Column("is_active")]
        [Comment("状態フラグ:true:利用可可能、null:削除済み")]
        public bool? IsAlive { get; set; } = true;
    }
}
