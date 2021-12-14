using Microsoft.EntityFrameworkCore.Migrations;

namespace kinder_app.Data.Migrations
{
    public partial class ItemUserIDstringEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            //migrationBuilder.AddColumn<string>(
            //    name: "Discriminator",
            //    table: "AspNetUsers",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<int>(
            //    name: "Karma_points",
            //    table: "AspNetUsers",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "AspNetUsers",
            //    type: "nvarchar(250)",
            //    maxLength: 250,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Surname",
            //    table: "AspNetUsers",
            //    type: "nvarchar(250)",
            //    maxLength: 250,
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Discriminator",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "Karma_points",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "Name",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "Surname",
            //    table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
