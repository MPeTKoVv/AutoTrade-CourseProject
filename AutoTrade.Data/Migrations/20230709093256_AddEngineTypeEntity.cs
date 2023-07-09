using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
    public partial class AddEngineTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("64be8597-ea30-4091-9f0a-7c31c98e311e"));

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Cars",
                newName: "AddedOn");

            migrationBuilder.AddColumn<int>(
                name: "EngineId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EngineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EngineTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Petrol" },
                    { 2, "Diesel" },
                    { 3, "Electric" },
                    { 4, "Hybrid" },
                    { 5, "Hydrogen Fuel Cell" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ConditionId", "Country", "CustomerId", "Description", "EngineId", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "Price", "SellerId", "Year" },
                values: new object[] { new Guid("b584d702-55b2-499b-ae76-877016e33252"), new DateTime(2023, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, 5, 1, "Germany", null, "This is my favorite car and the first my app :)", 1, 500, "https://i.ytimg.com/vi/i30EiaV-4_k/maxresdefault.jpg", "Mercedes", 0, "C 63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2023 });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_EngineId",
                table: "Cars",
                column: "EngineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_EngineTypes_EngineId",
                table: "Cars",
                column: "EngineId",
                principalTable: "EngineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_EngineTypes_EngineId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "EngineTypes");

            migrationBuilder.DropIndex(
                name: "IX_Cars_EngineId",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("b584d702-55b2-499b-ae76-877016e33252"));

            migrationBuilder.DropColumn(
                name: "EngineId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "AddedOn",
                table: "Cars",
                newName: "CreatedOn");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ApplicationUserId", "CategoryId", "ConditionId", "Country", "CreatedOn", "CustomerId", "Description", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "Price", "SellerId" },
                values: new object[] { new Guid("64be8597-ea30-4091-9f0a-7c31c98e311e"), null, 6, 1, "Germany", new DateTime(2023, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, "This is my favorite car and the first my app :)", 650, "https://i.ytimg.com/vi/i30EiaV-4_k/maxresdefault.jpg", "Mercedes", 0, "C63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801") });
        }
    }
}
