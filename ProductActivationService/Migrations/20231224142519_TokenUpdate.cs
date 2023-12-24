using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductActivationService.Migrations
{
    /// <inheritdoc />
    public partial class TokenUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Token",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "顧客ID");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Customer",
                type: "bigint",
                nullable: false,
                comment: "顧客ID",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ID")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Token");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Customer",
                type: "bigint",
                nullable: false,
                comment: "ID",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "顧客ID")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
