using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemPaiGo.Migrations
{
    /// <inheritdoc />
    public partial class seetypeslo : Migration
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
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_TypeSlots_TypeId",
                table: "Slots",
                column: "TypeId",
                principalTable: "TypeSlots",
                principalColumn: "Id");
        }
    }
}
