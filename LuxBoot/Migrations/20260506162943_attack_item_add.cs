using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuxBoot.Migrations
{
    /// <inheritdoc />
    public partial class attack_item_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAttacks",
                table: "AccountInfoModel");

            migrationBuilder.CreateTable(
                name: "AttackItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userIdId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttackType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountInfoModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttackItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttackItem_AccountInfoModel_AccountInfoModelId",
                        column: x => x.AccountInfoModelId,
                        principalTable: "AccountInfoModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttackItem_AspNetUsers_userIdId",
                        column: x => x.userIdId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttackItem_AccountInfoModelId",
                table: "AttackItem",
                column: "AccountInfoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AttackItem_userIdId",
                table: "AttackItem",
                column: "userIdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttackItem");

            migrationBuilder.AddColumn<string>(
                name: "CurrentAttacks",
                table: "AccountInfoModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
