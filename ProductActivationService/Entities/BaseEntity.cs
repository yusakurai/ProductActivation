using Microsoft.EntityFrameworkCore;

namespace ProductActivationService.Entities
{
    public class BaseEntity
    {
        [Comment("登録日時")]
        public DateTime? CreatedAt { get; set; }

        [Comment("更新日時")]
        public DateTime? UpdatedAt { get; set; }

        [Comment("削除日時")]
        public DateTime? DeletedAt { get; set; }
    }
}
