using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemPaiGo.Migrations
{
    /// <inheritdoc />
    public partial class relationprofiluser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ProfileTeachers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ProfileStudents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTeachers_UserId",
                table: "ProfileTeachers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileStudents_UserId",
                table: "ProfileStudents",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileStudents_Users_UserId",
                table: "ProfileStudents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileTeachers_Users_UserId",
                table: "ProfileTeachers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileStudents_Users_UserId",
                table: "ProfileStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileTeachers_Users_UserId",
                table: "ProfileTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ProfileTeachers_UserId",
                table: "ProfileTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ProfileStudents_UserId",
                table: "ProfileStudents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProfileTeachers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProfileStudents");
        }
    }
}
