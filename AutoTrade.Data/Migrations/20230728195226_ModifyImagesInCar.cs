using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoTrade.Data.Migrations
{
    public partial class ModifyImagesInCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("4cf16298-361a-4589-a957-e3bef610b9f9"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 28, 19, 52, 26, 204, DateTimeKind.Utc).AddTicks(577),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 27, 9, 7, 6, 466, DateTimeKind.Utc).AddTicks(9465));

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ApplicationUserId", "CategoryId", "ConditionId", "Country", "CustomerId", "Description", "EngineId", "Horsepower", "Make", "Mileage", "Model", "Price", "SellerId", "Year" },
                values: new object[] { new Guid("c042cf67-bdc6-48b4-881f-7face0154c22"), null, 5, 1, "Germany", null, "This is my favorite car and the first in my app :)", 1, 500, "Mercedes", 0, "C63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2023 });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c042cf67-bdc6-48b4-881f-7face0154c22"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 27, 9, 7, 6, 466, DateTimeKind.Utc).AddTicks(9465),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 28, 19, 52, 26, 204, DateTimeKind.Utc).AddTicks(577));

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ConditionId", "Country", "CustomerId", "Description", "EngineId", "Horsepower", "Make", "Mileage", "Model", "Price", "SellerId", "Year" },
                values: new object[] { new Guid("4cf16298-361a-4589-a957-e3bef610b9f9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, 1, "Germany", null, "This is my favorite car and the first in my app :)", 1, 500, "Mercedes", 0, "C63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2024 });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
