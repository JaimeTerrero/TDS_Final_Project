using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TDS_API.Migrations
{
    public partial class NewFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Peoples");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
