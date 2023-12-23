﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProductActivationService.Entities
{
  [Table("customer")]
  [Comment("カスタマー")]
  public class CustomerEntity : BaseEntity
  {
    [Key]
    [Required]
    [Column("id", TypeName = "bigint")]
    [Comment("ID")]
    public long Id { get; set; }

    [Required]
    [Column("name", TypeName = "varchar")]
    [StringLength(20)]
    [Comment("名称")]
    public string? Name { get; set; }
  }
}