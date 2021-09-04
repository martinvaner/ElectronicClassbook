using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddAbsentNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbsentNoteId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AbsentNoteStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsentNoteStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbsentNotes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsentNotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_AbsentNotes_AbsentNoteStates_StateId",
                        column: x => x.StateId,
                        principalTable: "AbsentNoteStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbsentNotes_Parent_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_AbsentNoteId",
                table: "Attendances",
                column: "AbsentNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_AbsentNotes_ParentId",
                table: "AbsentNotes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AbsentNotes_StateId",
                table: "AbsentNotes",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AbsentNotes_AbsentNoteId",
                table: "Attendances",
                column: "AbsentNoteId",
                principalTable: "AbsentNotes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AbsentNotes_AbsentNoteId",
                table: "Attendances");

            migrationBuilder.DropTable(
                name: "AbsentNotes");

            migrationBuilder.DropTable(
                name: "AbsentNoteStates");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_AbsentNoteId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "AbsentNoteId",
                table: "Attendances");
        }
    }
}
