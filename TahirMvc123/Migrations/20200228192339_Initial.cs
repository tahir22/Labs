using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TahirMvc123.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ProfilePic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vlilage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VlilageName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vlilage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cast",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CastName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    VlilageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cast_Vlilage_VlilageId",
                        column: x => x.VlilageId,
                        principalTable: "Vlilage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    CastId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Family_Cast_CastId",
                        column: x => x.CastId,
                        principalTable: "Cast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMember",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberName = table.Column<string>(nullable: true),
                    MarriedStatus = table.Column<bool>(nullable: false),
                    Children = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    FamilyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMember_Family_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Family",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyMember_FamilyMember_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FamilyMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_FamilyMember_FamilyId",
                table: "FamilyMember",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMember_ParentId",
                table: "FamilyMember",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "FamilyMember");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "Cast");

            migrationBuilder.DropTable(
                name: "Vlilage");
        }
    }
}
