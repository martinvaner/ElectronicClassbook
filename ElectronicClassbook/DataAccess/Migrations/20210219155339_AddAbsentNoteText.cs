using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddAbsentNoteText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "AbsentNotes",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "AbsentNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "AbsentNotes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AbsentNotes",
                newName: "id");
        }
    }
}
