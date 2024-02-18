using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemLotowMK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPaymentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 2, 20, 3, 39, 57, 540, DateTimeKind.Utc).AddTicks(8010), new DateTime(2024, 2, 19, 19, 39, 57, 540, DateTimeKind.Utc).AddTicks(8006) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 2, 21, 3, 39, 57, 540, DateTimeKind.Utc).AddTicks(8014), new DateTime(2024, 2, 20, 19, 39, 57, 540, DateTimeKind.Utc).AddTicks(8013) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 43, 3, 897, DateTimeKind.Utc).AddTicks(9273), new DateTime(2024, 2, 19, 9, 43, 3, 897, DateTimeKind.Utc).AddTicks(9269) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 2, 20, 17, 43, 3, 897, DateTimeKind.Utc).AddTicks(9277), new DateTime(2024, 2, 20, 9, 43, 3, 897, DateTimeKind.Utc).AddTicks(9277) });
        }
    }
}
