using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TahirMvc123.Migrations
{
    public partial class firstt22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vlilage",
                columns: table => new
                {
                    VlilageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VlilageName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vlilage", x => x.VlilageId);
                });

            migrationBuilder.CreateTable(
                name: "Cast",
                columns: table => new
                {
                    CastId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CastName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    VlilageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cast", x => x.CastId);
                    table.ForeignKey(
                        name: "FK_Cast_Vlilage_VlilageId",
                        column: x => x.VlilageId,
                        principalTable: "Vlilage",
                        principalColumn: "VlilageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    Familyid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    CastId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.Familyid);
                    table.ForeignKey(
                        name: "FK_Family_Cast_CastId",
                        column: x => x.CastId,
                        principalTable: "Cast",
                        principalColumn: "CastId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMember",
                columns: table => new
                {
                    Memberid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberName = table.Column<string>(nullable: true),
                    parentid = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Familyid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMember", x => x.Memberid);
                    table.ForeignKey(
                        name: "FK_FamilyMember_Family_Familyid",
                        column: x => x.Familyid,
                        principalTable: "Family",
                        principalColumn: "Familyid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cast_VlilageId",
                table: "Cast",
                column: "VlilageId");

            migrationBuilder.CreateIndex(
                name: "IX_Family_CastId",
                table: "Family",
                column: "CastId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMember_Familyid",
                table: "FamilyMember",
                column: "Familyid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyMember");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "Cast");

            migrationBuilder.DropTable(
                name: "Vlilage");
        }
    }
}
