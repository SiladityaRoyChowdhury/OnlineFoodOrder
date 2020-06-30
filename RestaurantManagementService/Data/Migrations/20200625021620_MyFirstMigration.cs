using Microsoft.EntityFrameworkCore.Migrations;

namespace Omf.RestaurantManagementService.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<string>(nullable: false),
                    Item = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    RestaurantId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 150, nullable: false),
                    Rating = table.Column<string>(maxLength: 15, nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    ListedCity = table.Column<string>(maxLength: 50, nullable: false),
                    ApproxCost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.RestaurantId);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantMenu",
                columns: table => new
                {
                    RestaurantId = table.Column<string>(nullable: false),
                    MenuId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantMenu", x => new { x.RestaurantId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_RestaurantMenu_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantMenu_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantMenu_MenuId",
                table: "RestaurantMenu",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantMenu");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Restaurant");
        }
    }
}
