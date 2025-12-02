using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SemPaiGo.Migrations
{
    /// <inheritdoc />
    public partial class paymentseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StatusTransactions",
                columns: new[] { "Id", "ArchivedAt", "Color", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("584f233a-bf58-4db9-a24e-90baac3f6d42"), null, "#ab69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Failed", null },
                    { new Guid("73f7fa42-196b-4040-b727-64a7b1e56458"), null, "#fa69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Paid", null },
                    { new Guid("ead0ecc1-a58d-4436-a87f-89c2dbc665a8"), null, "#ff69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Pending", null }
                });

            migrationBuilder.InsertData(
                table: "TypeTransactions",
                columns: new[] { "Id", "ArchivedAt", "Color", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("27aec3d1-dd41-4728-a29e-473da46779d9"), null, "#fa69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Payout", null },
                    { new Guid("50412518-6c82-40c1-b6bf-b9c7aadf67d1"), null, "#ab69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Refund", null },
                    { new Guid("7305a3d5-bfa9-41ce-be10-174c406cb842"), null, "#ff69b4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "Payment", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusTransactions",
                keyColumn: "Id",
                keyValue: new Guid("584f233a-bf58-4db9-a24e-90baac3f6d42"));

            migrationBuilder.DeleteData(
                table: "StatusTransactions",
                keyColumn: "Id",
                keyValue: new Guid("73f7fa42-196b-4040-b727-64a7b1e56458"));

            migrationBuilder.DeleteData(
                table: "StatusTransactions",
                keyColumn: "Id",
                keyValue: new Guid("ead0ecc1-a58d-4436-a87f-89c2dbc665a8"));

            migrationBuilder.DeleteData(
                table: "TypeTransactions",
                keyColumn: "Id",
                keyValue: new Guid("27aec3d1-dd41-4728-a29e-473da46779d9"));

            migrationBuilder.DeleteData(
                table: "TypeTransactions",
                keyColumn: "Id",
                keyValue: new Guid("50412518-6c82-40c1-b6bf-b9c7aadf67d1"));

            migrationBuilder.DeleteData(
                table: "TypeTransactions",
                keyColumn: "Id",
                keyValue: new Guid("7305a3d5-bfa9-41ce-be10-174c406cb842"));
        }
    }
}
