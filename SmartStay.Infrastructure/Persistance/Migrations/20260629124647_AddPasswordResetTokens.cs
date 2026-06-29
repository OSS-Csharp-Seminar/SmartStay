using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartStay.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordResetTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UserId",
                table: "PasswordResetTokens",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

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

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("097758cd-3def-4b95-8d79-491a56f818b9"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(2216), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("44d360a3-7433-4405-aaf2-32c2a3eebdf5"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(1768), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("487b0929-3600-450c-928a-5d0e9bcefaec"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(2182), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(2213), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("cf5106c1-8fe1-4889-b905-c7810f2eb519"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2026, 6, 13, 12, 55, 37, 417, DateTimeKind.Unspecified).AddTicks(2210), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "amenities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1f78345f-fa5f-4ef8-898e-2e6afc355131"), "Pool" },
                    { new Guid("aedfa227-3169-436a-a0c0-7b6c4e656972"), "Breakfast" },
                    { new Guid("bb06550e-d7b6-4a21-b91f-8cdae43dbf7b"), "Spa" },
                    { new Guid("d3b7bccf-8e8e-4df7-9163-23752ade6e5b"), "Gym" },
                    { new Guid("e38c5313-a4bc-435b-a942-0a7cb495ba87"), "Garage" }
                });
        }
    }
}
