using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SemPaiGo.Migrations
{
    /// <inheritdoc />
    public partial class seetypeslots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_TypeSlot_TypeId",
                table: "Slots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeSlot",
                table: "TypeSlot");

            migrationBuilder.RenameTable(
                name: "TypeSlot",
                newName: "TypeSlots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeSlots",
                table: "TypeSlots",
                column: "Id");

            migrationBuilder.InsertData(
                table: "TypeSlots",
                columns: new[] { "Id", "ArchivedAt", "Color", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("4043e32b-4d92-49b5-b885-505155ff2fe9"), null, "#fa69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "pi pi-desktop", "Visio", null },
                    { new Guid("79f538c3-5f2b-4e45-a5f8-4d7cda8b3df8"), null, "#ff69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "pi pi-arrow-down-left-and-arrow-up-right-to-center", "Presentiel", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_TypeSlots_TypeId",
                table: "Slots",
                column: "TypeId",
                principalTable: "TypeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_TypeSlots_TypeId",
                table: "Slots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeSlots",
                table: "TypeSlots");

            migrationBuilder.DeleteData(
                table: "TypeSlots",
                keyColumn: "Id",
                keyValue: new Guid("4043e32b-4d92-49b5-b885-505155ff2fe9"));

            migrationBuilder.DeleteData(
                table: "TypeSlots",
                keyColumn: "Id",
                keyValue: new Guid("79f538c3-5f2b-4e45-a5f8-4d7cda8b3df8"));

            migrationBuilder.RenameTable(
                name: "TypeSlots",
                newName: "TypeSlot");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeSlot",
                table: "TypeSlot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_TypeSlot_TypeId",
                table: "Slots",
                column: "TypeId",
                principalTable: "TypeSlot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
