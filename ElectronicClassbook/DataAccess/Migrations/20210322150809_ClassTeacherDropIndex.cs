using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ClassTeacherDropIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClassTeacher_ClassId",
                table: "ClassTeacher");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacher_ClassId",
                table: "ClassTeacher",
                column: "ClassId",
                unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClassTeacher_ClassId",
                table: "ClassTeacher");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacher_ClassId",
                table: "ClassTeacher",
                column: "ClassId",
                unique: true);
        }
    }
}
