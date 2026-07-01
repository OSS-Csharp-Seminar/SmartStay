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
