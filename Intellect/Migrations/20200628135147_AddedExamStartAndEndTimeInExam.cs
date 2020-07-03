using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intellect.Migrations
{
    public partial class AddedExamStartAndEndTimeInExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExamEndDateTime",
                table: "Exams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExamStartDateTime",
                table: "Exams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamEndDateTime",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "ExamStartDateTime",
                table: "Exams");
        }
    }
}
