﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
    public partial class AddOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("365c4dd8-3755-47de-bdca-788b5d901def"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CategoryId", "Description", "EngineId", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "OwnerId", "Price", "SellerId", "TransmissionId", "Year" },
                values: new object[] { new Guid("e520542b-5b05-4f5f-a515-299dd2c14905"), 5, "This is my favorite car and the first in my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", "Mercedes", 0, "C63 AMG", new Guid("e44e2759-94b3-4441-b2d4-4d9dd3260cb6"), 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2, 2023 });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("e520542b-5b05-4f5f-a515-299dd2c14905"));

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Description", "EngineId", "Horsepower", "ImageUrl", "IsActive", "IsForSale", "Make", "Mileage", "Model", "Price", "SellerId", "TransmissionId", "Year" },
                values: new object[] { new Guid("365c4dd8-3755-47de-bdca-788b5d901def"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "This is my favorite car and the first in my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", false, false, "Mercedes", 0, "C63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2, 2023 });
        }
    }
}
