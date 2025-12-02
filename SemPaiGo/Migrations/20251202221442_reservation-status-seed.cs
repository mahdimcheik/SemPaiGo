using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SemPaiGo.Migrations
{
    /// <inheritdoc />
    public partial class reservationstatusseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "StatusReservation");

            migrationBuilder.InsertData(
                table: "StatusReservation",
                columns: new[] { "Id", "ArchivedAt", "Color", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2ec60a91-aab8-4753-a5d8-b131b9441e77"), null, "#ff69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Pendind", null },
                    { new Guid("32d854b6-6d4e-445a-9209-31a492970f2d"), null, "#fa69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Accepted", null },
                    { new Guid("6d281aec-d093-4071-8bf4-c8363361b5b4"), null, "#ab69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Done", null },
                    { new Guid("caab85f5-d37b-4ea0-b035-5ba3ca8dd49f"), null, "#ab69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Rejected", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusReservation",
                keyColumn: "Id",
                keyValue: new Guid("2ec60a91-aab8-4753-a5d8-b131b9441e77"));

            migrationBuilder.DeleteData(
                table: "StatusReservation",
                keyColumn: "Id",
                keyValue: new Guid("32d854b6-6d4e-445a-9209-31a492970f2d"));

            migrationBuilder.DeleteData(
                table: "StatusReservation",
                keyColumn: "Id",
                keyValue: new Guid("6d281aec-d093-4071-8bf4-c8363361b5b4"));

            migrationBuilder.DeleteData(
                table: "StatusReservation",
                keyColumn: "Id",
                keyValue: new Guid("caab85f5-d37b-4ea0-b035-5ba3ca8dd49f"));

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "StatusReservation",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
