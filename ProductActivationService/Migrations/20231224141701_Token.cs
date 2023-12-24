using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductActivationService.Migrations
{
    /// <inheritdoc />
    public partial class Token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Sub = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "sub"),
                    ClientGuid = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "クライアントGUID"),
                    ClientHostName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "クライアントホスト名"),
                    Jwt = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "JWT"),
                    Exp = table.Column<long>(type: "bigint", nullable: false, comment: "有効期限"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "登録日時"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "更新日時"),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "削除日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Sub);
                },
                comment: "トークン");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token");
        }
    }
}
