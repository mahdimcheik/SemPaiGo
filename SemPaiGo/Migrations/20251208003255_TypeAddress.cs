using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SempaiGo.Migrations
{
    /// <inheritdoc />
    public partial class TypeAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TypeAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ArchivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAddresses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TypeAddresses",
                columns: new[] { "Id", "ArchivedAt", "Color", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b8b8a8fc-ca60-440b-815f-1e44b89c9803"), null, "#fa69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Billing", null },
                    { new Guid("e1fee3ea-6190-48c3-8e40-c1f053fea79d"), null, "#ff69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Home", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TypeId",
                table: "Addresses",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_TypeAddresses_TypeId",
                table: "Addresses",
                column: "TypeId",
                principalTable: "TypeAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_TypeAddresses_TypeId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "TypeAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_TypeId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Addresses");
        }
    }
}
