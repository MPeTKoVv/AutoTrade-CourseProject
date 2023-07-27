using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
    public partial class ModifyImageColumnInCarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("6a1b9174-c810-44a6-b157-d495c7de3e75"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cars");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 9, 12, 24, 50, 988, DateTimeKind.Utc).AddTicks(7828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 9, 10, 20, 50, 688, DateTimeKind.Utc).AddTicks(776));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Cars",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ApplicationUserId", "CategoryId", "ConditionId", "Country", "CustomerId", "Description", "EngineId", "Horsepower", "Image", "Make", "Mileage", "Model", "Price", "SellerId", "Year" },
                values: new object[] { new Guid("27b768cc-fa3f-487b-9afb-2eacefee4ad8"), null, 5, 1, "Germany", null, "This is my favorite car and the first my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", "Mercedes", 0, "C 63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2023 });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("27b768cc-fa3f-487b-9afb-2eacefee4ad8"));

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cars");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 9, 10, 20, 50, 688, DateTimeKind.Utc).AddTicks(776),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 9, 12, 24, 50, 988, DateTimeKind.Utc).AddTicks(7828));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ConditionId", "Country", "CustomerId", "Description", "EngineId", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "Price", "SellerId", "Year" },
                values: new object[] { new Guid("6a1b9174-c810-44a6-b157-d495c7de3e75"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, 1, "Germany", null, "This is my favorite car and the first my app :)", 1, 500, "https://i.ytimg.com/vi/i30EiaV-4_k/maxresdefault.jpg", "Mercedes", 0, "C 63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2023 });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
