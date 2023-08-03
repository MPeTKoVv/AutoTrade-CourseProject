using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
    public partial class ChangeCollectionOfImagesToImageUrlPropCarEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_CarId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ApplicationUserId",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c042cf67-bdc6-48b4-881f-7face0154c22"));

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "CarId",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId1",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 28, 19, 52, 26, 204, DateTimeKind.Utc).AddTicks(577));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "Cars",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: true);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CategoryId", "ConditionId", "Country", "CustomerId", "Description", "EngineId", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "Price", "SellerId", "Year" },
                values: new object[] { new Guid("45da5b78-7a91-4c58-ac8a-ec9e0f4571b4"), 5, 1, "Germany", null, "This is my favorite car and the first in my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", "Mercedes", 0, "C63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2023 });

            migrationBuilder.CreateIndex(
                name: "IX_Images_CarId1",
                table: "Images",
                column: "CarId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Images_Cars_CarId1",
            //    table: "Images",
            //    column: "CarId1",
            //    principalTable: "Cars",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cars_CarId1",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_CarId1",
                table: "Images");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("45da5b78-7a91-4c58-ac8a-ec9e0f4571b4"));

            migrationBuilder.DropColumn(
                name: "CarId1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cars");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 28, 19, 52, 26, 204, DateTimeKind.Utc).AddTicks(577),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ConditionId", "Country", "CustomerId", "Description", "EngineId", "Horsepower", "Make", "Mileage", "Model", "Price", "SellerId", "Year" },
                values: new object[] { new Guid("c042cf67-bdc6-48b4-881f-7face0154c22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, 1, "Germany", null, "This is my favorite car and the first in my app :)", 1, 500, "Mercedes", 0, "C63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2023 });

            migrationBuilder.CreateIndex(
                name: "IX_Images_CarId",
                table: "Images",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ApplicationUserId",
                table: "Cars",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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
