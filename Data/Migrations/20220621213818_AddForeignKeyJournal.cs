using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddForeignKeyJournal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdUser",
                table: "Journal",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_IdUser",
                table: "Journal",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_AspNetUsers_IdUser",
                table: "Journal",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_AspNetUsers_IdUser",
                table: "Journal");

            migrationBuilder.DropIndex(
                name: "IX_Journal_IdUser",
                table: "Journal");

            migrationBuilder.AlterColumn<string>(
                name: "IdUser",
                table: "Journal",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
