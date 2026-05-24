using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuxBoot.Migrations
{
    /// <inheritdoc />
    public partial class current_attacks_add_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentAttacks",
                table: "AccountInfoModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAttacks",
                table: "AccountInfoModel");
        }
    }
}
