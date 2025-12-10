using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SempaiGo.Migrations
{
    /// <inheritdoc />
    public partial class socialmedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaceBook",
                table: "ProfileTeachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GitHub",
                table: "ProfileTeachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "ProfileTeachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "ProfileTeachers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceBook",
                table: "ProfileTeachers");

            migrationBuilder.DropColumn(
                name: "GitHub",
                table: "ProfileTeachers");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "ProfileTeachers");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "ProfileTeachers");
        }
    }
}
