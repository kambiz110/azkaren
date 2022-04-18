using Microsoft.EntityFrameworkCore.Migrations;

namespace Azmoon.Persistence.Migrations
{
    public partial class update_tbl_quizfilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DarajehOption",
                table: "QuizFilters");

            migrationBuilder.AddColumn<long>(
                name: "QuizId",
                table: "QuizFilters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "TypeDarajeh",
                table: "QuizFilters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuizFilters_QuizId",
                table: "QuizFilters",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizFilters_Quizzes_QuizId",
                table: "QuizFilters",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizFilters_Quizzes_QuizId",
                table: "QuizFilters");

            migrationBuilder.DropIndex(
                name: "IX_QuizFilters_QuizId",
                table: "QuizFilters");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "QuizFilters");

            migrationBuilder.DropColumn(
                name: "TypeDarajeh",
                table: "QuizFilters");

            migrationBuilder.AddColumn<string>(
                name: "DarajehOption",
                table: "QuizFilters",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
