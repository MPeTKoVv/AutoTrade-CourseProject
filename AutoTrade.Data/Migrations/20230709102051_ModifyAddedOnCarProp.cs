using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
    public partial class ModifyAddedOnCarProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("1e4aee76-110c-4cf5-a976-11ad2d42a492"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 9, 10, 20, 50, 688, DateTimeKind.Utc).AddTicks(776),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ApplicationUserId", "CategoryId", "ConditionId", "Country", "CustomerId", "Description", "EngineId", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "Price", "SellerId", "Year" },
                values: new object[] { new Guid("6a1b9174-c810-44a6-b157-d495c7de3e75"), null, 5, 1, "Germany", null, "This is my favorite car and the first my app :)", 1, 500, "https://i.ytimg.com/vi/i30EiaV-4_k/maxresdefault.jpg", "Mercedes", 0, "C 63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2023 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("6a1b9174-c810-44a6-b157-d495c7de3e75"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 9, 10, 20, 50, 688, DateTimeKind.Utc).AddTicks(776));

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ConditionId", "Country", "CustomerId", "Description", "EngineId", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "Price", "SellerId", "Year" },
                values: new object[] { new Guid("1e4aee76-110c-4cf5-a976-11ad2d42a492"), new DateTime(2023, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, 5, 1, "Germany", null, "This is my favorite car and the first my app :)", 1, 500, "https://i.ytimg.com/vi/i30EiaV-4_k/maxresdefault.jpg", "Mercedes", 0, "C 63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2023 });
        }
    }
}
