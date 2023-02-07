using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TDS_API.Migrations
{
    public partial class AddingAnimalAndOtherImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActualFileUrl",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActualFileUrl",
                table: "Others",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualFileUrl",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ActualFileUrl",
                table: "Others");
        }
    }
}
