using Microsoft.EntityFrameworkCore.Migrations;

namespace kinder_app.Data.Migrations
{
    public partial class UpdateLiked2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LikedItems",
                table: "LikedItems");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "LikedItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "LikedItems",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikedItems",
                table: "LikedItems",
                column: "Key");
        }
    }
}
