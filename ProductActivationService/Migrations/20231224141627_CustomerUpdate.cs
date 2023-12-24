using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductActivationService.Migrations
{
    /// <inheritdoc />
    public partial class CustomerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customer",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "顧客名",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "名称");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customer",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "顧客名");
        }
    }
}
