using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("9c7080a0-adf2-40c0-bba7-3c53ac575c71"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WalletId" },
                values: new object[,]
                {
                    { new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474"), 0, "f13641de-f809-4fe2-b636-2f2292b82daa", "admin2@autotrade.com", false, "Great", "Admin", false, null, "admin2@autotrade.com", "admin2@autotrade.com", "AQAAAAEAACcQAAAAEBdE2gTCTKn9vJKxrYoHibFjpEin2NJrzI5Qog1mIlkIOPvuATFD/GOzrlRErUISNg==", null, false, null, false, "admin2@autotrade.com", null },
                    { new Guid("365fea60-9416-452c-90bf-69b9e632dcd6"), 0, "1c9a96b5-3c78-42a3-b5e3-8f4479ffb5a1", "gosho@user.com", false, "Gosho", "Georgiev", false, null, "gosho@user.com", "gosho@user.com", "AQAAAAEAACcQAAAAEKpbZsdv437Y++O1kz0zZIYC6Qg4nFTRSw0ENPwzictMIr6znMEXgVI42F+mcDeMEQ==", null, false, null, false, "gosho@user.com", null },
                    { new Guid("633475e7-ab79-49b2-a198-43e1777e929f"), 0, "84b25b00-b2e9-48a3-976b-5d997c8fbdae", "pesho@user.com", false, "Pesho", "Petrov", false, null, "pesho@user.com", "pesho@user.com", "AQAAAAEAACcQAAAAEJvMzmmpYgsIFY+1tSnkLbjbJ7iLm05Jdvc3cr6UXcf9bRUPpS3NG/X6/OZrGqWwPw==", null, false, null, false, "pesho@user.com", null }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CategoryId", "Description", "EngineId", "Horsepower", "ImageUrl", "Make", "Mileage", "Model", "OwnerId", "Price", "SellerId", "TransmissionId", "Year" },
                values: new object[] { new Guid("cb832dfd-1f1d-464b-9a7a-fb8781ed137d"), 5, "This is my favorite car and the first in my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", "Mercedes", 0, "C63 AMG", new Guid("e44e2759-94b3-4441-b2d4-4d9dd3260cb6"), 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2, 2023 });

            migrationBuilder.InsertData(
                table: "Sellers",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { new Guid("27ffd22c-2eca-461b-b85d-d5cc53478446"), "+359888888888", new Guid("633475e7-ab79-49b2-a198-43e1777e929f") });

            migrationBuilder.InsertData(
                table: "Sellers",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { new Guid("fe1effcf-f870-426e-b248-90d057a40ab0"), "+359888888888", new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("365fea60-9416-452c-90bf-69b9e632dcd6"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("cb832dfd-1f1d-464b-9a7a-fb8781ed137d"));

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: new Guid("27ffd22c-2eca-461b-b85d-d5cc53478446"));

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: new Guid("fe1effcf-f870-426e-b248-90d057a40ab0"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("633475e7-ab79-49b2-a198-43e1777e929f"));

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AddedOn", "AddedOnForSale", "CategoryId", "Description", "EngineId", "Horsepower", "ImageUrl", "IsActive", "IsForSale", "Make", "Mileage", "Model", "OwnerId", "Price", "SellerId", "TransmissionId", "Year" },
                values: new object[] { new Guid("9c7080a0-adf2-40c0-bba7-3c53ac575c71"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "This is my favorite car and the first in my app :)", 1, 500, "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg", false, false, "Mercedes", 0, "C63 AMG", new Guid("e44e2759-94b3-4441-b2d4-4d9dd3260cb6"), 150000m, new Guid("cdb33d65-5b4b-4dec-899b-32e2b843f801"), 2, 2023 });
        }
    }
}
