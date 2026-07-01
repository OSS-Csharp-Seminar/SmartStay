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
         

            migrationBuilder.AddColumn<Guid>(
                name: "RenterId",
                table: "Rooms",
                type: "uuid",
                nullable: true
                );
            
            //M.G: filling rest of nonNullvalues so error doesnt show up
            migrationBuilder.Sql(@"
        UPDATE ""Rooms""
        SET ""RenterId"" = '019f1512-dab6-7ac3-97df-da69ea9fadc3'
        WHERE ""RenterId"" IS NULL;
    ");

            migrationBuilder.AlterColumn<Guid>(
                name: "RenterId",
                table: "Rooms",
                type: "uuid",
                nullable: false); 
            
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
            
            migrationBuilder.DropColumn(
                name: "RenterId",
                table: "Rooms");
        }
    }
}
