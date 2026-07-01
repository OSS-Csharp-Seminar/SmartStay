using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartStay.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRenterDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_RenterId",
                table: "Rooms");

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("256d5bde-c7dc-45ea-a357-31c171d1df9d"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("618bfb12-002e-40f4-ae8c-9e02dffd1484"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("b154c649-3194-4a5b-b9d1-2d3ae06c6e35"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("b2e57b8d-a882-45b8-9484-91e2ab8d4322"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("fec3770b-b67c-4641-b176-af7a37249f3c"));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("097758cd-3def-4b95-8d79-491a56f818b9"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 57, 37, 128, DateTimeKind.Unspecified).AddTicks(7107), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 57, 37, 128, DateTimeKind.Unspecified).AddTicks(6765), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("487b0929-3600-450c-928a-5d0e9bcefaec"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 57, 37, 128, DateTimeKind.Unspecified).AddTicks(7095), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 57, 37, 128, DateTimeKind.Unspecified).AddTicks(7104), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("cf5106c1-8fe1-4889-b905-c7810f2eb519"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 57, 37, 128, DateTimeKind.Unspecified).AddTicks(7101), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "amenities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2e91381a-50b3-451c-b634-fbe4b583a3cc"), "Pool" },
                    { new Guid("5d672992-9c9b-4fea-9bbf-87919570b642"), "Spa" },
                    { new Guid("6929d098-55f2-4766-adb5-faa959da725f"), "Gym" },
                    { new Guid("9e197332-511f-48ab-9bfb-c33e3e46ee0b"), "Garage" },
                    { new Guid("dd4fd08b-5ab1-40aa-b056-6e9164404518"), "Breakfast" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_RenterId",
                table: "Rooms",
                column: "RenterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_RenterId",
                table: "Rooms");

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("2e91381a-50b3-451c-b634-fbe4b583a3cc"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("5d672992-9c9b-4fea-9bbf-87919570b642"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("6929d098-55f2-4766-adb5-faa959da725f"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("9e197332-511f-48ab-9bfb-c33e3e46ee0b"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("dd4fd08b-5ab1-40aa-b056-6e9164404518"));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("097758cd-3def-4b95-8d79-491a56f818b9"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(2052), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(1602), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("487b0929-3600-450c-928a-5d0e9bcefaec"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(1999), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(2048), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("cf5106c1-8fe1-4889-b905-c7810f2eb519"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(2044), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "amenities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("256d5bde-c7dc-45ea-a357-31c171d1df9d"), "Garage" },
                    { new Guid("618bfb12-002e-40f4-ae8c-9e02dffd1484"), "Spa" },
                    { new Guid("b154c649-3194-4a5b-b9d1-2d3ae06c6e35"), "Gym" },
                    { new Guid("b2e57b8d-a882-45b8-9484-91e2ab8d4322"), "Breakfast" },
                    { new Guid("fec3770b-b67c-4641-b176-af7a37249f3c"), "Pool" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_RenterId",
                table: "Rooms",
                column: "RenterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
