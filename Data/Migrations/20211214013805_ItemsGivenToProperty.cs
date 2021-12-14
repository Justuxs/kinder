using Microsoft.EntityFrameworkCore.Migrations;

namespace kinder_app.Data.Migrations
{
    public partial class ItemsGivenToProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GivenTo",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GivenTo",
                table: "Item");
        }
    }
}
