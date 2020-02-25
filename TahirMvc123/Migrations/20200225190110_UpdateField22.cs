using Microsoft.EntityFrameworkCore.Migrations;

namespace TahirMvc123.Migrations
{
    public partial class UpdateField22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MarriedStatus",
                table: "FamilyMember",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarriedStatus",
                table: "FamilyMember");
        }
    }
}
