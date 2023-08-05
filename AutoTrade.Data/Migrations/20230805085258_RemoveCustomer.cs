using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
	public partial class RemoveCustomer : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Cars_AspNetUsers_CustomerId",
				table: "Cars");

			migrationBuilder.DeleteData(
				table: "Cars",
				keyColumn: "Id",
				keyValue: new Guid("fba9419d-4a5d-4d56-be58-0d2a4ee72064"));

			migrationBuilder.DropColumn(
				name: "CustomerId",
				table: "Cars");

			migrationBuilder.DropColumn(
				name: "ApplicationUserId",
				table: "Cars");

			migrationBuilder.DropIndex(
				name: "IX_Cars_CustomerId",
				table: "Cars");

			migrationBuilder.InsertData(
				table: "Cars",
				columns: new[] { "Id", "ApplicationUserId", "CategoryId", "Description", "EngineId", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "Price", "SellerId", "TransmissionId", "Year" },
				values: new object[] { new Guid("28e57985-11b9-4110-8f06-2ede1405c2e4"), null, 5, "This is my favorite car and the first in my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", "Mercedes", 0, "C63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2, 2023 });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Cars_AspNetUsers_ApplicationUserId",
				table: "Cars");

			migrationBuilder.DeleteData(
				table: "Cars",
				keyColumn: "Id",
				keyValue: new Guid("28e57985-11b9-4110-8f06-2ede1405c2e4"));

			migrationBuilder.RenameColumn(
				name: "ApplicationUserId",
				table: "Cars",
				newName: "CustomerId");

			migrationBuilder.RenameIndex(
				name: "IX_Cars_ApplicationUserId",
				table: "Cars",
				newName: "IX_Cars_CustomerId");

			migrationBuilder.InsertData(
				table: "Cars",
				columns: new[] { "Id", "AddedOn", "CategoryId", "CustomerId", "Description", "EngineId", "Horsepower", "ImageUrl", "IsActive", "IsForSale", "Make", "Mileage", "Model", "Price", "SellerId", "TransmissionId", "Year" },
				values: new object[] { new Guid("fba9419d-4a5d-4d56-be58-0d2a4ee72064"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, "This is my favorite car and the first in my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", false, false, "Mercedes", 0, "C63 AMG", 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2, 2023 });

			migrationBuilder.AddForeignKey(
				name: "FK_Cars_AspNetUsers_CustomerId",
				table: "Cars",
				column: "CustomerId",
				principalTable: "AspNetUsers",
				principalColumn: "Id");
		}
	}
}
