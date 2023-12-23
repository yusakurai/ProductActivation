using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProductActivation.Service.Models
{
  [Table("customer")]
  [Comment("カスタマーテーブル")]
  public class Customer : BaseEntity
  {
    [Key]
    [Required]
    [Column("id", TypeName = "bigint")]
    [Comment("サンプルID")]
    public long SampleId { get; set; }

    [Required]
    [Column("sample_name", TypeName = "varchar")]
    [StringLength(20)]
    [Comment("サンプル名称")]
    public long SampleName { get; set; }
  }
}