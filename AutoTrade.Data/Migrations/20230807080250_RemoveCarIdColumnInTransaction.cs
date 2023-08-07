using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
    public partial class RemoveCarIdColumnInTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("ff2ac97c-83e8-4e64-8951-78f940fb8d1b"));

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Transactions");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CategoryId", "Description", "EngineId", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "OwnerId", "Price", "SellerId", "TransmissionId", "Year" },
                values: new object[] { new Guid("097a1a90-eb12-45de-a1cb-01b984b5409b"), 5, "This is my favorite car and the first in my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", "Mercedes", 0, "C63 AMG", new Guid("e44e2759-94b3-4441-b2d4-4d9dd3260cb6"), 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2, 2023 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("097a1a90-eb12-45de-a1cb-01b984b5409b"));

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AddedOn", "AddedOnForSale", "CategoryId", "Description", "EngineId", "Horsepower", "ImageUrl", "IsActive", "IsForSale", "Make", "Mileage", "Model", "OwnerId", "Price", "SellerId", "TransmissionId", "Year" },
                values: new object[] { new Guid("ff2ac97c-83e8-4e64-8951-78f940fb8d1b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "This is my favorite car and the first in my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", false, false, "Mercedes", 0, "C63 AMG", new Guid("e44e2759-94b3-4441-b2d4-4d9dd3260cb6"), 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2, 2023 });
        }
    }
}
