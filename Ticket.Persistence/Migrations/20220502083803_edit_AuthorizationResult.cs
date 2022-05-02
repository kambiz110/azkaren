using Microsoft.EntityFrameworkCore.Migrations;

namespace Azmoon.Persistence.Migrations
{
    public partial class edit_AuthorizationResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorizationResult",
                table: "Results",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizationResult",
                table: "Results");
        }
    }
}
