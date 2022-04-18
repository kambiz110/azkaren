using Microsoft.EntityFrameworkCore.Migrations;

namespace Azmoon.Persistence.Migrations
{
    public partial class update_tbl_quiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuizFilters_QuizId",
                table: "QuizFilters");

            migrationBuilder.AddColumn<long>(
                name: "QuizFilterId",
                table: "Quizzes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizFilters_QuizId",
                table: "QuizFilters",
                column: "QuizId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuizFilters_QuizId",
                table: "QuizFilters");

            migrationBuilder.DropColumn(
                name: "QuizFilterId",
                table: "Quizzes");

            migrationBuilder.CreateIndex(
                name: "IX_QuizFilters_QuizId",
                table: "QuizFilters",
                column: "QuizId");
        }
    }
}
