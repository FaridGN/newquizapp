using Microsoft.EntityFrameworkCore.Migrations;

namespace Intellect.Migrations
{
    public partial class EmptyMigrationToFixConflicts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_TestTakers_Exams_ExamId",
            //    table: "TestTakers");

            //migrationBuilder.DropIndex(
            //    name: "IX_TestTakers_ExamId",
            //    table: "TestTakers");

            //migrationBuilder.DropColumn(
            //    name: "ExamId",
            //    table: "TestTakers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "ExamId",
            //    table: "TestTakers",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTakers_ExamId",
            //    table: "TestTakers",
            //    column: "ExamId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TestTakers_Exams_ExamId",
            //    table: "TestTakers",
            //    column: "ExamId",
            //    principalTable: "Exams",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
