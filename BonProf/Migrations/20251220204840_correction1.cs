using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonProf.Migrations
{
    /// <inheritdoc />
    public partial class correction1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Profiles_ProfileId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Formations_Profiles_ProfileId",
                table: "Formations");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Profiles_ProfileId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ProfileStudents_StudentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Genders_GenderId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileStudents_Users_UserId",
                table: "ProfileStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ProfileStudents_StudentId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileStudents",
                table: "ProfileStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.RenameTable(
                name: "ProfileStudents",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "Profile");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileStudents_UserId",
                table: "Students",
                newName: "IX_Students_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Profiles_UserId",
                table: "Profile",
                newName: "IX_Profile_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Profiles_GenderId",
                table: "Profile",
                newName: "IX_Profile_GenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profile",
                table: "Profile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Profile_ProfileId",
                table: "Addresses",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Formations_Profile_ProfileId",
                table: "Formations",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Profile_ProfileId",
                table: "Languages",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Students_StudentId",
                table: "Orders",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Genders_GenderId",
                table: "Profile",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Users_UserId",
                table: "Profile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Students_StudentId",
                table: "Reservations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Profile_ProfileId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Formations_Profile_ProfileId",
                table: "Formations");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Profile_ProfileId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Students_StudentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Genders_GenderId",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Users_UserId",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Students_StudentId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profile",
                table: "Profile");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "ProfileStudents");

            migrationBuilder.RenameTable(
                name: "Profile",
                newName: "Profiles");

            migrationBuilder.RenameIndex(
                name: "IX_Students_UserId",
                table: "ProfileStudents",
                newName: "IX_ProfileStudents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Profile_UserId",
                table: "Profiles",
                newName: "IX_Profiles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Profile_GenderId",
                table: "Profiles",
                newName: "IX_Profiles_GenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileStudents",
                table: "ProfileStudents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Profiles_ProfileId",
                table: "Addresses",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Formations_Profiles_ProfileId",
                table: "Formations",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Profiles_ProfileId",
                table: "Languages",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ProfileStudents_StudentId",
                table: "Orders",
                column: "StudentId",
                principalTable: "ProfileStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Genders_GenderId",
                table: "Profiles",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileStudents_Users_UserId",
                table: "ProfileStudents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ProfileStudents_StudentId",
                table: "Reservations",
                column: "StudentId",
                principalTable: "ProfileStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
