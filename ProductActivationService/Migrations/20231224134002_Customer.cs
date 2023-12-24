using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductActivationService.Migrations
{
    /// <inheritdoc />
    public partial class Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "名称"),
                    ProductKey = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false, comment: "プロダクトキー"),
                    LicenseLimit = table.Column<int>(type: "int", nullable: false, comment: "ライセンス数"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "登録日時"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "更新日時"),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "削除日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                },
                comment: "顧客");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
