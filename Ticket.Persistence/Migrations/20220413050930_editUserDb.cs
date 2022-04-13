using Microsoft.EntityFrameworkCore.Migrations;

namespace Azmoon.Persistence.Migrations
{
    public partial class editUserDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "darajeh",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "melli",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_father",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tavalod",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "darajeh",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "melli",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "name_father",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "tavalod",
                table: "AspNetUsers");
        }
    }
}
