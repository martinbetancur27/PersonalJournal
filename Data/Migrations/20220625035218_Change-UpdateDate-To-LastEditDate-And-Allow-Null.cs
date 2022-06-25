using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class ChangeUpdateDateToLastEditDateAndAllowNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Journal");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Comment");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditDate",
                table: "Note",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditDate",
                table: "Journal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditDate",
                table: "Comment",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEditDate",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "LastEditDate",
                table: "Journal");

            migrationBuilder.DropColumn(
                name: "LastEditDate",
                table: "Comment");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Journal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
