using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SempaiGo.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileStudents_Languages_LanguageId",
                table: "ProfileStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileTeachers_Languages_LanguageId",
                table: "ProfileTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ProfileTeachers_LanguageId",
                table: "ProfileTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ProfileStudents_LanguageId",
                table: "ProfileStudents");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "ProfileTeachers");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "ProfileStudents");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Languages");

            migrationBuilder.CreateTable(
                name: "TeachersXLanguages",
                columns: table => new
                {
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersXLanguages", x => new { x.LanguageId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_TeachersXLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachersXLanguages_ProfileTeachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "ProfileTeachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "ArchivedAt", "Color", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3aa916ed-53d2-4f93-80e9-b49171a7ebe1"), null, "#ab69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Anglais", null },
                    { new Guid("52b54b82-1f37-4a66-a263-708b53cd685d"), null, "#fa69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Francais", null },
                    { new Guid("ff34f5ba-6201-45bf-9217-dcda019976a3"), null, "#ff69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Arabe", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachersXLanguages_TeacherId",
                table: "TeachersXLanguages",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeachersXLanguages");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("3aa916ed-53d2-4f93-80e9-b49171a7ebe1"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("52b54b82-1f37-4a66-a263-708b53cd685d"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("ff34f5ba-6201-45bf-9217-dcda019976a3"));

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "ProfileTeachers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "ProfileStudents",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Languages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTeachers_LanguageId",
                table: "ProfileTeachers",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileStudents_LanguageId",
                table: "ProfileStudents",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileStudents_Languages_LanguageId",
                table: "ProfileStudents",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileTeachers_Languages_LanguageId",
                table: "ProfileTeachers",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }
    }
}
