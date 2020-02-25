using Microsoft.EntityFrameworkCore.Migrations;

namespace TahirMvc123.Migrations
{
    public partial class UpdateField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FamilyMember_parentid",
                table: "FamilyMember",
                column: "parentid");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMember_FamilyMember_parentid",
                table: "FamilyMember",
                column: "parentid",
                principalTable: "FamilyMember",
                principalColumn: "Memberid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMember_FamilyMember_parentid",
                table: "FamilyMember");

            migrationBuilder.DropIndex(
                name: "IX_FamilyMember_parentid",
                table: "FamilyMember");
        }
    }
}
