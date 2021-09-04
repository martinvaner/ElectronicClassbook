using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddStudentAbsentNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "AbsentNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AbsentNotes_StudentId",
                table: "AbsentNotes",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsentNotes_Student_StudentId",
                table: "AbsentNotes",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsentNotes_Student_StudentId",
                table: "AbsentNotes");

            migrationBuilder.DropIndex(
                name: "IX_AbsentNotes_StudentId",
                table: "AbsentNotes");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "AbsentNotes");
        }
    }
}
