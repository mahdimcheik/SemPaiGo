using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemPaiGo.Migrations
{
    /// <inheritdoc />
    public partial class seetypeslotss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_TypeSlots_TypeId",
                table: "Slots");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeId",
                table: "Slots",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "TypeSlots",
                columns: new[] { "Id", "ArchivedAt", "Color", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[] { new Guid("c25c18a2-af88-4132-a27a-0025417edb56"), null, "#fa69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "pi pi-crown", "Tous", null });

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_TypeSlots_TypeId",
                table: "Slots",
                column: "TypeId",
                principalTable: "TypeSlots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_TypeSlots_TypeId",
                table: "Slots");

            migrationBuilder.DeleteData(
                table: "TypeSlots",
                keyColumn: "Id",
                keyValue: new Guid("c25c18a2-af88-4132-a27a-0025417edb56"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeId",
                table: "Slots",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_TypeSlots_TypeId",
                table: "Slots",
                column: "TypeId",
                principalTable: "TypeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
