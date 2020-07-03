using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intellect.Migrations
{
    public partial class UpdatedUserQuestionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestions_Questions_QuestionId",
                table: "UserQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestions_TestTakers_TestTakerId",
                table: "UserQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuestions",
                table: "UserQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "UserQuestions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TestTakerId",
                table: "UserQuestions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserQuestionId",
                table: "UserQuestions",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "UserQuestions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "UserQuestions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "UserQuestions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOfQuestion",
                table: "UserQuestions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimeSpend",
                table: "UserQuestions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuestions",
                table: "UserQuestions",
                column: "UserQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_AnswerId",
                table: "UserQuestions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_ExamId",
                table: "UserQuestions",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_TestTakerId",
                table: "UserQuestions",
                column: "TestTakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestions_Answers_AnswerId",
                table: "UserQuestions",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestions_Exams_ExamId",
                table: "UserQuestions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestions_Questions_QuestionId",
                table: "UserQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestions_TestTakers_TestTakerId",
                table: "UserQuestions",
                column: "TestTakerId",
                principalTable: "TestTakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestions_Answers_AnswerId",
                table: "UserQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestions_Exams_ExamId",
                table: "UserQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestions_Questions_QuestionId",
                table: "UserQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestions_TestTakers_TestTakerId",
                table: "UserQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuestions",
                table: "UserQuestions");

            migrationBuilder.DropIndex(
                name: "IX_UserQuestions_AnswerId",
                table: "UserQuestions");

            migrationBuilder.DropIndex(
                name: "IX_UserQuestions_ExamId",
                table: "UserQuestions");

            migrationBuilder.DropIndex(
                name: "IX_UserQuestions_TestTakerId",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "UserQuestionId",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "TimeOfQuestion",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "TimeSpend",
                table: "UserQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "TestTakerId",
                table: "UserQuestions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "UserQuestions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuestions",
                table: "UserQuestions",
                columns: new[] { "TestTakerId", "QuestionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestions_Questions_QuestionId",
                table: "UserQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestions_TestTakers_TestTakerId",
                table: "UserQuestions",
                column: "TestTakerId",
                principalTable: "TestTakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
