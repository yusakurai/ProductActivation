﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductActivationService.Data;

#nullable disable

namespace ProductActivationService.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20231224142519_TokenUpdate")]
    partial class TokenUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductActivationService.Entities.CustomerEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasComment("顧客ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasComment("登録日時");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasComment("削除日時");

                    b.Property<int>("LicenseLimit")
                        .HasColumnType("int")
                        .HasComment("ライセンス数");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasComment("顧客名");

                    b.Property<string>("ProductKey")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasComment("プロダクトキー");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasComment("更新日時");

                    b.HasKey("Id");

                    b.ToTable("Customer", t =>
                        {
                            t.HasComment("顧客");
                        });
                });

            modelBuilder.Entity("ProductActivationService.Entities.TokenEntity", b =>
                {
                    b.Property<Guid>("Sub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("sub");

                    b.Property<string>("ClientGuid")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasComment("クライアントGUID");

                    b.Property<string>("ClientHostName")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasComment("クライアントホスト名");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasComment("登録日時");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint")
                        .HasComment("顧客ID");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasComment("削除日時");

                    b.Property<long>("Exp")
                        .HasColumnType("bigint")
                        .HasComment("有効期限");

                    b.Property<string>("Jwt")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("JWT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasComment("更新日時");

                    b.HasKey("Sub");

                    b.ToTable("Token", t =>
                        {
                            t.HasComment("トークン");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
