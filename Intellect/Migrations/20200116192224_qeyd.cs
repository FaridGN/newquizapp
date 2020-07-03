using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intellect.Migrations
{
    public partial class qeyd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TestTakers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birth",
                table: "TestTakers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "TestTakers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "TestTakers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TestTakers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth",
                table: "TestTakers");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "TestTakers");

            migrationBuilder.DropColumn(
                name: "Job",
                table: "TestTakers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TestTakers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TestTakers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
