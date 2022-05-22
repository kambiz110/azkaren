using Microsoft.EntityFrameworkCore.Migrations;

namespace Azmoon.Persistence.Migrations
{
    public partial class editworkplace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortIndex",
                table: "WorkPlaces",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortIndex",
                table: "WorkPlaces");
        }
    }
}
