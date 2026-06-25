using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartStay.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Rooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Latitude = table.Column<double>(type: "numeric(9,6)", nullable: false),
                    Longitude = table.Column<double>(type: "numeric(9,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "Country", "Latitude", "Longitude", "PostalCode" },
                values: new object[,]
                {
                    { new Guid("44fd2891-34ea-4a44-b8c3-f2716cd744e6"), "123 Main Street", "New York", "United States", 40.712800000000001, -74.006, "10001" },
                    { new Guid("64a6ac13-7659-4434-9c39-403e98d7aa7e"), "456 Oxford Street", "London", "United Kingdom", 51.507399999999997, -0.1278, "SW1A 1AA" },
                    { new Guid("d106dc17-6a8c-4e91-8355-9a1a756f7833"), "789 Champs-Élysées", "Paris", "France", 48.8566, 2.3521999999999998, "75008" },
                    { new Guid("ebbff419-195d-4b57-af48-fac84d93f482"), "321 Shibuya Crossing", "Tokyo", "Japan", 35.676200000000001, 139.65029999999999, "150-0043" },
                    { new Guid("f6aa2e4e-64b6-4608-b73d-89cb699f0382"), "555 Harbour Bridge Road", "Sydney", "Australia", -33.8688, 151.20930000000001, "2000" }
                });

            migrationBuilder.InsertData(
                table: "amenities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1f78345f-fa5f-4ef8-898e-2e6afc355131"), "Pool" },
                    { new Guid("44fd1192-34ea-4a44-b8c3-f2716cd744e6"), "Wifi" },
                    { new Guid("48fd1813-3344-4a45-42c2-f2718cd741e5"), "Fireplace" },
                    { new Guid("48fd1822-33ea-4a44-b8e3-f2716dd744e6"), "Air conditioning" },
                    { new Guid("48fd1893-3344-4a45-b7c3-f271acd744e6"), "Jacuzzi" },
                    { new Guid("aedfa227-3169-436a-a0c0-7b6c4e656972"), "Breakfast" },
                    { new Guid("bb06550e-d7b6-4a21-b91f-8cdae43dbf7b"), "Spa" },
                    { new Guid("d3b7bccf-8e8e-4df7-9163-23752ade6e5b"), "Gym" },
                    { new Guid("e38c5313-a4bc-435b-a942-0a7cb495ba87"), "Garage" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AverageRating", "BedType", "Capacity", "CreatedAt", "Description", "LocationId", "Name", "PricePerNight", "Size" },
                values: new object[,]
                {
                    { new Guid("097758cd-3def-4b95-8d79-491a56f818b9"), 4.2f, "Single", 2, new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(2216), new TimeSpan(0, 0, 0, 0, 0)), "Compact yet comfortable studio with kitchenette", new Guid("f6aa2e4e-64b6-4608-b73d-89cb699f0382"), "Studio Apartment", 120f, 25 },
                    { new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5"), 4.8f, "King", 2, new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(1768), new TimeSpan(0, 0, 0, 0, 0)), "Spacious room with stunning ocean views", new Guid("44fd2891-34ea-4a44-b8c3-f2716cd744e6"), "Deluxe Ocean View", 250f, 45 },
                    { new Guid("487b0929-3600-450c-928a-5d0e9bcefaec"), 4.6f, "Queen", 4, new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(2182), new TimeSpan(0, 0, 0, 0, 0)), "Perfect for families, with two bedrooms and a living area", new Guid("64a6ac13-7659-4434-9c39-403e98d7aa7e"), "Family Suite", 350f, 75 },
                    { new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"), 4.9f, "King", 2, new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(2213), new TimeSpan(0, 0, 0, 0, 0)), "Cozy room perfect for couples with fireplace and jacuzzi", new Guid("ebbff419-195d-4b57-af48-fac84d93f482"), "Romantic Getaway", 300f, 40 },
                    { new Guid("cf5106c1-8fe1-4889-b905-c7810f2eb519"), 4.4f, "Single", 2, new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(2210), new TimeSpan(0, 0, 0, 0, 0)), "Ideal for business travelers with work desk and high-speed internet", new Guid("d106dc17-6a8c-4e91-8355-9a1a756f7833"), "Business Executive", 180f, 30 }
                });

            migrationBuilder.InsertData(
                table: "room_amenities",
                columns: new[] { "AmenityId", "RoomId" },
                values: new object[,]
                {
                    { new Guid("44fd1192-34ea-4a44-b8c3-f2716cd744e6"), new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5") },
                    { new Guid("48fd1822-33ea-4a44-b8e3-f2716dd744e6"), new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5") },
                    { new Guid("48fd1813-3344-4a45-42c2-f2718cd741e5"), new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db") },
                    { new Guid("48fd1893-3344-4a45-b7c3-f271acd744e6"), new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_LocationId",
                table: "Rooms",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_RoomAvailability",
                table: "Bookings",
                columns: new[] { "RoomId", "CheckinDate", "CheckOutDate", "Status" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Locations_LocationId",
                table: "Rooms",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Locations_LocationId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_LocationId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Booking_RoomAvailability",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("097758cd-3def-4b95-8d79-491a56f818b9"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("487b0929-3600-450c-928a-5d0e9bcefaec"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("cf5106c1-8fe1-4889-b905-c7810f2eb519"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("1f78345f-fa5f-4ef8-898e-2e6afc355131"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("aedfa227-3169-436a-a0c0-7b6c4e656972"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("bb06550e-d7b6-4a21-b91f-8cdae43dbf7b"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("d3b7bccf-8e8e-4df7-9163-23752ade6e5b"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("e38c5313-a4bc-435b-a942-0a7cb495ba87"));

            migrationBuilder.DeleteData(
                table: "room_amenities",
                keyColumns: new[] { "AmenityId", "RoomId" },
                keyValues: new object[] { new Guid("44fd1192-34ea-4a44-b8c3-f2716cd744e6"), new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5") });

            migrationBuilder.DeleteData(
                table: "room_amenities",
                keyColumns: new[] { "AmenityId", "RoomId" },
                keyValues: new object[] { new Guid("48fd1822-33ea-4a44-b8e3-f2716dd744e6"), new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5") });

            migrationBuilder.DeleteData(
                table: "room_amenities",
                keyColumns: new[] { "AmenityId", "RoomId" },
                keyValues: new object[] { new Guid("48fd1813-3344-4a45-42c2-f2718cd741e5"), new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db") });

            migrationBuilder.DeleteData(
                table: "room_amenities",
                keyColumns: new[] { "AmenityId", "RoomId" },
                keyValues: new object[] { new Guid("48fd1893-3344-4a45-b7c3-f271acd744e6"), new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db") });

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("44fd1192-34ea-4a44-b8c3-f2716cd744e6"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("48fd1813-3344-4a45-42c2-f2718cd741e5"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("48fd1822-33ea-4a44-b8e3-f2716dd744e6"));

            migrationBuilder.DeleteData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: new Guid("48fd1893-3344-4a45-b7c3-f271acd744e6"));

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
