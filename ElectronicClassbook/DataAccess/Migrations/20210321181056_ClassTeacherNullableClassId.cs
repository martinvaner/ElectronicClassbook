using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
	public partial class ClassTeacherNullableClassId : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ClassTeacher_Classes_ClassId",
				table: "ClassTeacher");

			migrationBuilder.DropIndex(
				name: "IX_ClassTeacher_ClassId",
				table: "ClassTeacher");

			migrationBuilder.AlterColumn<int>(
				name: "ClassId",
				table: "ClassTeacher",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.CreateIndex(
				name: "IX_ClassTeacher_ClassId",
				table: "ClassTeacher",
				column: "ClassId",
				unique: true);

			migrationBuilder.AddForeignKey(
				name: "FK_ClassTeacher_Classes_ClassId",
				table: "ClassTeacher",
				column: "ClassId",
				principalTable: "Classes",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ClassTeacher_Classes_ClassId",
				table: "ClassTeacher");

			migrationBuilder.DropIndex(
				name: "IX_ClassTeacher_ClassId",
				table: "ClassTeacher");

			migrationBuilder.AlterColumn<int>(
				name: "ClassId",
				table: "ClassTeacher",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_ClassTeacher_ClassId",
				table: "ClassTeacher",
				column: "ClassId",
				unique: true);

			migrationBuilder.AddForeignKey(
				name: "FK_ClassTeacher_Classes_ClassId",
				table: "ClassTeacher",
				column: "ClassId",
				principalTable: "Classes",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
