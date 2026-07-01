using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartStay.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddRenterIdToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("64ff4bbb-e98d-46a7-ba17-48db9e46745f"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("6ebfc71e-5ab8-4b5c-b340-05b143a49082"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("8d9f9fd3-779b-4f0a-bca1-d23f3cc46d1b"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("b85d64b7-fe63-4a61-9378-53e933b392ad"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("d659ae8a-1743-4568-8d48-dd81ef609271"));

            migrationBuilder.AddColumn<Guid>(
                name: "RenterId",
                table: "Rooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("097758cd-3def-4b95-8d79-491a56f818b9"),
                columns: new[] { "CreatedAt", "RenterId" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(2052), new TimeSpan(0, 0, 0, 0, 0)), new Guid("019e666d-05fb-7de1-9964-45e0e028a38a") });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5"),
                columns: new[] { "CreatedAt", "RenterId" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(1602), new TimeSpan(0, 0, 0, 0, 0)), new Guid("019e666d-05fb-7de1-9964-45e0e028a38a") });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("487b0929-3600-450c-928a-5d0e9bcefaec"),
                columns: new[] { "CreatedAt", "RenterId" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(1999), new TimeSpan(0, 0, 0, 0, 0)), new Guid("019e666d-05fb-7de1-9964-45e0e028a38a") });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"),
                columns: new[] { "CreatedAt", "RenterId" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(2048), new TimeSpan(0, 0, 0, 0, 0)), new Guid("019e666d-05fb-7de1-9964-45e0e028a38a") });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("cf5106c1-8fe1-4889-b905-c7810f2eb519"),
                columns: new[] { "CreatedAt", "RenterId" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 1, 11, 22, 42, 173, DateTimeKind.Unspecified).AddTicks(2044), new TimeSpan(0, 0, 0, 0, 0)), new Guid("019e666d-05fb-7de1-9964-45e0e028a38a") });

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

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RenterId",
                table: "Rooms",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_RenterId",
                table: "Rooms",
                column: "RenterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_RenterId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RenterId",
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

            migrationBuilder.DropColumn(
                name: "RenterId",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("097758cd-3def-4b95-8d79-491a56f818b9"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 29, 12, 46, 47, 19, DateTimeKind.Unspecified).AddTicks(4600), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 29, 12, 46, 47, 19, DateTimeKind.Unspecified).AddTicks(4143), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("487b0929-3600-450c-928a-5d0e9bcefaec"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 29, 12, 46, 47, 19, DateTimeKind.Unspecified).AddTicks(4553), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 29, 12, 46, 47, 19, DateTimeKind.Unspecified).AddTicks(4597), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("cf5106c1-8fe1-4889-b905-c7810f2eb519"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 29, 12, 46, 47, 19, DateTimeKind.Unspecified).AddTicks(4593), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "amenities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("64ff4bbb-e98d-46a7-ba17-48db9e46745f"), "Pool" },
                    { new Guid("6ebfc71e-5ab8-4b5c-b340-05b143a49082"), "Spa" },
                    { new Guid("8d9f9fd3-779b-4f0a-bca1-d23f3cc46d1b"), "Gym" },
                    { new Guid("b85d64b7-fe63-4a61-9378-53e933b392ad"), "Garage" },
                    { new Guid("d659ae8a-1743-4568-8d48-dd81ef609271"), "Breakfast" }
                });
        }
    }
}
