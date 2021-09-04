using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddShowToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Show",
                table: "SchoolHomeNotes",
                newName: "ShowStudent");

            migrationBuilder.RenameColumn(
                name: "Show",
                table: "Homework",
                newName: "ShowStudent");

            migrationBuilder.AddColumn<bool>(
                name: "ShowParent",
                table: "SchoolHomeNotes",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowParent",
                table: "Homework",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowParent",
                table: "SchoolHomeNotes");

            migrationBuilder.DropColumn(
                name: "ShowParent",
                table: "Homework");

            migrationBuilder.RenameColumn(
                name: "ShowStudent",
                table: "SchoolHomeNotes",
                newName: "Show");

            migrationBuilder.RenameColumn(
                name: "ShowStudent",
                table: "Homework",
                newName: "Show");
        }
    }
}
