using Microsoft.EntityFrameworkCore.Migrations;

namespace kinder_app.Data.Migrations
{
    public partial class AddUserFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false);
            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false);
            migrationBuilder.AddColumn<int>(
                name: "Karma_points",
                table: "AspNetUsers",
                type: "int",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
            migrationBuilder.DropColumn(
                name: "Karma_points",
                table: "AspNetUsers");
        }
    }
}
