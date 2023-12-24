using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductActivationService.Migrations
{
    /// <inheritdoc />
    public partial class TokenUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jwt",
                table: "Token");

            migrationBuilder.AlterColumn<string>(
                name: "ClientGuid",
                table: "Token",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                comment: "クライアントGUID",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldComment: "クライアントGUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClientGuid",
                table: "Token",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                comment: "クライアントGUID",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldComment: "クライアントGUID");

            migrationBuilder.AddColumn<string>(
                name: "Jwt",
                table: "Token",
                type: "nvarchar(max)",
                nullable: true,
                comment: "JWT");
        }
    }
}
