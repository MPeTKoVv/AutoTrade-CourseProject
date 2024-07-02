using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
    public partial class SeedWallets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c4ee72ce-9905-46cf-a400-0d7fedeb4ea8"));

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: new Guid("4acae7ff-c6af-4024-982b-00d977aff591"));

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: new Guid("9e808a99-24c1-4e45-9951-a0b50c85d676"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "WalletId" },
                values: new object[] { "c1bcb009-d3ea-4238-9e95-c284a8ecee34", "AQAAAAEAACcQAAAAEMqOLicoZmD3/Qg4fhYiL4r5rHrEDQmAiiAEscN5ChmeHJXkjnnq0BWJEHzm5+UsiQ==", "admin2@autotrade.com", new Guid("668fdb29-7f70-493b-9f76-3de7bda803b1") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("633475e7-ab79-49b2-a198-43e1777e929f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "WalletId" },
                values: new object[] { "ca2cc912-6016-4506-aa04-b5cbdcfda73f", "AQAAAAEAACcQAAAAEGZGQ4rpaEdD7YOTKm4N4bMl65lPsS2e79BD1xtC6O6Z7fjADPFG2zy1j41WZ3Q8fg==", "pesho@user.com", new Guid("d8afd034-d2f3-4a71-b7f0-782c901a6a21") });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WalletId" },
                values: new object[] { new Guid("C4EE72CE-9905-46CF-A400-0D7FEDEB4EA8"), 0, "ab1ebe06-f9a1-4f59-8c6e-cebebdb62a55", "gosho@user.com", false, "Gosho", "Georgiev", false, null, "gosho@user.com", "gosho@user.com", "AQAAAAEAACcQAAAAEMrPZg3wdaFceF/s+ZASiUJLwDMgYY5REtvDspQEA7UEtt9+n5coYTWKPMnhaN1aOg==", null, false, "gosho@user.com", false, "gosho@user.com", new Guid("b7af5e8d-1056-4f4f-b463-1133dba03d81") });

            migrationBuilder.InsertData(
                table: "Sellers",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("9E808A99-24C1-4E45-9951-A0B50C85D676"), "+359888888888", new Guid("633475e7-ab79-49b2-a198-43e1777e929f") },
                    { new Guid("4ACAE7FF-C6AF-4024-982B-00D977AFF591"), "+359888888888", new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474") }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "CreditCardId", "UserId" },
                values: new object[,]
                {
                    { new Guid("668fdb29-7f70-493b-9f76-3de7bda803b1"), 100000000m, null, new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474") },
                    { new Guid("b7af5e8d-1056-4f4f-b463-1133dba03d81"), 150000m, null, new Guid("c4ee72ce-9905-46cf-a400-0d7fedeb4ea8") },
                    { new Guid("d8afd034-d2f3-4a71-b7f0-782c901a6a21"), 500000m, null, new Guid("633475e7-ab79-49b2-a198-43e1777e929f") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("260d6dd3-78aa-41da-8ad8-cd0e4d9cba9a"));

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: new Guid("4fa77724-5a0e-4e0c-bd65-894827926e7e"));

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: new Guid("aee396d6-acb2-4daa-9de0-270532d9a770"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("668fdb29-7f70-493b-9f76-3de7bda803b1"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("b7af5e8d-1056-4f4f-b463-1133dba03d81"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("d8afd034-d2f3-4a71-b7f0-782c901a6a21"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "WalletId" },
                values: new object[] { "a7b66d5a-80e8-4ea3-a77a-f439f8b7b1e9", "AQAAAAEAACcQAAAAELDc2jXhDUN+LarnTvltuNqeTFgRaA9iM7h23dJsGpoCdFbGwwtToeiJlOX2JpKHmw==", "gosho@user.com", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("633475e7-ab79-49b2-a198-43e1777e929f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "WalletId" },
                values: new object[] { "ef59f5fc-47f1-4026-99aa-b1acce9c6e96", "AQAAAAEAACcQAAAAEISau/0WDSmNTfQrpasM11g4Jgz7HS5Cgw3vUXIxS/X5XaV592RiqM/kgkOe8z4x6g==", "gosho@user.com", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WalletId" },
                values: new object[] { new Guid("c4ee72ce-9905-46cf-a400-0d7fedeb4ea8"), 0, "223cc139-87d0-45d6-b799-253f1650b859", "gosho@user.com", false, "Gosho", "Georgiev", false, null, "gosho@user.com", "gosho@user.com", "AQAAAAEAACcQAAAAEBKrrbiMfDtGMiFgVtQl3+hY5zzkSYTY2+mU+/IRtmxk8xD2nQOctiQCmWUUJsPZVQ==", null, false, "gosho@user.com", false, "gosho@user.com", null });

            migrationBuilder.InsertData(
                table: "Sellers",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("4acae7ff-c6af-4024-982b-00d977aff591"), "+359888888888", new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474") },
                    { new Guid("9e808a99-24c1-4e45-9951-a0b50c85d676"), "+359888888888", new Guid("633475e7-ab79-49b2-a198-43e1777e929f") }
                });
        }
    }
}
