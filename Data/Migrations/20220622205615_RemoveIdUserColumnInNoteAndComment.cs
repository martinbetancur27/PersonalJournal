using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RemoveIdUserColumnInNoteAndComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Comment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "Note",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
