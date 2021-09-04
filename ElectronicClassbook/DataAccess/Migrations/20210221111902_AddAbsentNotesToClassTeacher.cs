using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddAbsentNotesToClassTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsentNotes_Parent_ParentId",
                table: "AbsentNotes");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "AbsentNotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClassTeacherId",
                table: "AbsentNotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbsentNotes_ClassTeacherId",
                table: "AbsentNotes",
                column: "ClassTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsentNotes_ClassTeacher_ClassTeacherId",
                table: "AbsentNotes",
                column: "ClassTeacherId",
                principalTable: "ClassTeacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbsentNotes_Parent_ParentId",
                table: "AbsentNotes",
                column: "ParentId",
                principalTable: "Parent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsentNotes_ClassTeacher_ClassTeacherId",
                table: "AbsentNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AbsentNotes_Parent_ParentId",
                table: "AbsentNotes");

            migrationBuilder.DropIndex(
                name: "IX_AbsentNotes_ClassTeacherId",
                table: "AbsentNotes");

            migrationBuilder.DropColumn(
                name: "ClassTeacherId",
                table: "AbsentNotes");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "AbsentNotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbsentNotes_Parent_ParentId",
                table: "AbsentNotes",
                column: "ParentId",
                principalTable: "Parent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
