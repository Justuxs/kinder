using Microsoft.EntityFrameworkCore.Migrations;

namespace kinder_app.Data.Migrations
{
    public partial class addLikedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikedItems",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedItems");
        }
    }
}
