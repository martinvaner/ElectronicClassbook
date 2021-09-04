using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddingParentStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Parent_ParentsId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Student_ChildrensId",
                table: "ParentStudent");

            migrationBuilder.RenameColumn(
                name: "ParentsId",
                table: "ParentStudent",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "ChildrensId",
                table: "ParentStudent",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentStudent_ParentsId",
                table: "ParentStudent",
                newName: "IX_ParentStudent_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Parent_ParentId",
                table: "ParentStudent",
                column: "ParentId",
                principalTable: "Parent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Student_StudentId",
                table: "ParentStudent",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Parent_ParentId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Student_StudentId",
                table: "ParentStudent");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "ParentStudent",
                newName: "ParentsId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "ParentStudent",
                newName: "ChildrensId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentStudent_StudentId",
                table: "ParentStudent",
                newName: "IX_ParentStudent_ParentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Parent_ParentsId",
                table: "ParentStudent",
                column: "ParentsId",
                principalTable: "Parent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Student_ChildrensId",
                table: "ParentStudent",
                column: "ChildrensId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
