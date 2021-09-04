using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FixClassClassTeacherRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_ClassTeacher_ClassId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_ClassId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "ClassTeacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacher_ClassId",
                table: "ClassTeacher",
                column: "ClassId",
                unique: true,
                filter: "[ClassId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeacher_Classes_ClassId",
                table: "ClassTeacher",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeacher_Classes_ClassId",
                table: "ClassTeacher");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeacher_ClassId",
                table: "ClassTeacher");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "ClassTeacher");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassId",
                table: "Classes",
                column: "ClassId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_ClassTeacher_ClassId",
                table: "Classes",
                column: "ClassId",
                principalTable: "ClassTeacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
