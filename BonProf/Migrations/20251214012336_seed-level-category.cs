using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SemPaiGo.Migrations
{
    /// <inheritdoc />
    public partial class seedlevelcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoryCursuses",
                columns: new[] { "Id", "ArchivedAt", "Color", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0b6865c4-76fc-4caa-8171-07f449ca6e5c"), null, "#fa69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Front-end", null },
                    { new Guid("4133e2f8-f0e0-44f8-8cfb-8a8e1aaa86d7"), null, "#ff69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Back-end", null },
                    { new Guid("86969bd8-e51f-4558-ac44-fc159ed31c53"), null, "#ab69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Software", null },
                    { new Guid("a7f8b05d-3d2d-43fa-870d-987c21f4e41d"), null, "#ab69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Techniques", null }
                });

            migrationBuilder.InsertData(
                table: "LevelCursuses",
                columns: new[] { "Id", "ArchivedAt", "Color", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("19702cef-0a3b-46f8-932b-cf78634e741d"), null, "#ab69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Intermédiaire", null },
                    { new Guid("a518f61e-8a19-44e6-b365-fb285ad0811e"), null, "#ab69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Avancé", null },
                    { new Guid("b70134eb-060c-4069-a325-1251c75a1ac9"), null, "#ff69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Tous niveaux", null },
                    { new Guid("eb4bd576-6855-4123-bb92-e921c8610542"), null, "#fa69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Débutant", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryCursuses",
                keyColumn: "Id",
                keyValue: new Guid("0b6865c4-76fc-4caa-8171-07f449ca6e5c"));

            migrationBuilder.DeleteData(
                table: "CategoryCursuses",
                keyColumn: "Id",
                keyValue: new Guid("4133e2f8-f0e0-44f8-8cfb-8a8e1aaa86d7"));

            migrationBuilder.DeleteData(
                table: "CategoryCursuses",
                keyColumn: "Id",
                keyValue: new Guid("86969bd8-e51f-4558-ac44-fc159ed31c53"));

            migrationBuilder.DeleteData(
                table: "CategoryCursuses",
                keyColumn: "Id",
                keyValue: new Guid("a7f8b05d-3d2d-43fa-870d-987c21f4e41d"));

            migrationBuilder.DeleteData(
                table: "LevelCursuses",
                keyColumn: "Id",
                keyValue: new Guid("19702cef-0a3b-46f8-932b-cf78634e741d"));

            migrationBuilder.DeleteData(
                table: "LevelCursuses",
                keyColumn: "Id",
                keyValue: new Guid("a518f61e-8a19-44e6-b365-fb285ad0811e"));

            migrationBuilder.DeleteData(
                table: "LevelCursuses",
                keyColumn: "Id",
                keyValue: new Guid("b70134eb-060c-4069-a325-1251c75a1ac9"));

            migrationBuilder.DeleteData(
                table: "LevelCursuses",
                keyColumn: "Id",
                keyValue: new Guid("eb4bd576-6855-4123-bb92-e921c8610542"));
        }
    }
}
