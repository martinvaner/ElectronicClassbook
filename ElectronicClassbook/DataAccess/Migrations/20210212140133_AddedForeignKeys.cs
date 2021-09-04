using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classbooks_Classes_ClassId",
                table: "Classbooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Teacher_CreatedById",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Records",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Records_CreatedById",
                table: "Records",
                newName: "IX_Records_TeacherId");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Classbooks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classbooks_Classes_ClassId",
                table: "Classbooks",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Teacher_TeacherId",
                table: "Records",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classbooks_Classes_ClassId",
                table: "Classbooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Teacher_TeacherId",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Records",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Records_TeacherId",
                table: "Records",
                newName: "IX_Records_CreatedById");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Classbooks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Classbooks_Classes_ClassId",
                table: "Classbooks",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Teacher_CreatedById",
                table: "Records",
                column: "CreatedById",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
