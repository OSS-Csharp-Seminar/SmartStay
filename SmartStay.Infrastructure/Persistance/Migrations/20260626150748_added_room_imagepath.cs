using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartStay.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class added_room_imagepath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Rooms",
                type: "character varying(260)",
                maxLength: 260,
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Rooms"
                );
        }
    }
}
