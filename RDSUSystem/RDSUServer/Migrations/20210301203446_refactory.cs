using Microsoft.EntityFrameworkCore.Migrations;

namespace RDSUServer.Migrations
{
    public partial class refactory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DanceCategory",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "OldCategory",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "isSt",
                table: "Categories",
                newName: "Category");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Scorecards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Scorecards");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Categories",
                newName: "isSt");

            migrationBuilder.AddColumn<byte>(
                name: "DanceCategory",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "OldCategory",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
