using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omf.ReviewManagementService.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewDateTime = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<string>(maxLength: 15, nullable: false),
                    Comments = table.Column<string>(maxLength: 500, nullable: false),
                    RestaurantId = table.Column<string>(maxLength: 10, nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
