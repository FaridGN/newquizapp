using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intellect.Migrations
{
    public partial class testtaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Remainedtime",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Questions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TestTakerId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestTakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Result = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserQuestions",
                columns: table => new
                {
                    TestTakerId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestions", x => new { x.TestTakerId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_UserQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserQuestions_TestTakers_TestTakerId",
                        column: x => x.TestTakerId,
                        principalTable: "TestTakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TestTakerId",
                table: "AspNetUsers",
                column: "TestTakerId",
                unique: true,
                filter: "[TestTakerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_QuestionId",
                table: "UserQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TestTakers_TestTakerId",
                table: "AspNetUsers",
                column: "TestTakerId",
                principalTable: "TestTakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TestTakers_TestTakerId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserQuestions");

            migrationBuilder.DropTable(
                name: "TestTakers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TestTakerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TestTakerId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Remainedtime",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);
        }
    }
}
