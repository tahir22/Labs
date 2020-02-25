using Microsoft.EntityFrameworkCore.Migrations;

namespace TahirMvc123.Migrations
{
    public partial class UpdateField2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "FamilyMember",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Children",
                table: "FamilyMember");
        }
    }
}
