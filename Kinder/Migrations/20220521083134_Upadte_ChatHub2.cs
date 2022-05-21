using Microsoft.EntityFrameworkCore.Migrations;

namespace kinder_app.Migrations
{
    public partial class Upadte_ChatHub2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved1",
                table: "ChatHubs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved2",
                table: "ChatHubs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved1",
                table: "ChatHubs");

            migrationBuilder.DropColumn(
                name: "Approved2",
                table: "ChatHubs");
        }
    }
}
