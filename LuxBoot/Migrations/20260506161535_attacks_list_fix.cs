using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuxBoot.Migrations
{
    /// <inheritdoc />
    public partial class attacks_list_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attacks",
                table: "AccountInfoModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attacks",
                table: "AccountInfoModel");
        }
    }
}
