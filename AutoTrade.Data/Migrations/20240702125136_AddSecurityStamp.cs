using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTrade.Data.Migrations
{
    public partial class AddSecurityStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1609d5ac-cf56-4fa3-8c17-3fc19b36f4ad"));

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: new Guid("65fe3d63-c50b-4499-89e4-99b5f98640f0"));

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: new Guid("ae885d42-849e-49c8-81b5-4fbeac003182"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7b66d5a-80e8-4ea3-a77a-f439f8b7b1e9", "AQAAAAEAACcQAAAAELDc2jXhDUN+LarnTvltuNqeTFgRaA9iM7h23dJsGpoCdFbGwwtToeiJlOX2JpKHmw==", "admin2@autotrade.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("633475e7-ab79-49b2-a198-43e1777e929f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef59f5fc-47f1-4026-99aa-b1acce9c6e96", "AQAAAAEAACcQAAAAEISau/0WDSmNTfQrpasM11g4Jgz7HS5Cgw3vUXIxS/X5XaV592RiqM/kgkOe8z4x6g==", "pesho@user.com" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36812a96-1d32-4e8c-8dfb-7cd6b6f3299a", "AQAAAAEAACcQAAAAECaiEJW6owYgLFr4UvN0ktZ4mFk5/uu9V05cgNw7esGDWx7lGzGiFnZ0KtTna5J3Pg==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("633475e7-ab79-49b2-a198-43e1777e929f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8bfe02cc-345c-4ffd-a399-ef8ac5beb05b", "AQAAAAEAACcQAAAAECSRPqJqPDCkhKYVNzXFPDLZzRxP5gi3uRKmZEiV1aVT67/qkXqsBs1Qf4PlabJdzQ==", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WalletId" },
                values: new object[] { new Guid("1609d5ac-cf56-4fa3-8c17-3fc19b36f4ad"), 0, "21b8fa04-fe73-4144-bd73-cb3552eb9463", "gosho@user.com", false, "Gosho", "Georgiev", false, null, "gosho@user.com", "gosho@user.com", "AQAAAAEAACcQAAAAEPAyubF0Ek2F9GPwmsy4F2KVOOKYmSmsWB3U02i0zZc5sftydNrBeG8g5hylNK3P0Q==", null, false, null, false, "gosho@user.com", null });

            migrationBuilder.InsertData(
                table: "Sellers",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("65fe3d63-c50b-4499-89e4-99b5f98640f0"), "+359888888888", new Guid("1f995e54-2e2d-4d3c-b612-2ea74a593474") },
                    { new Guid("ae885d42-849e-49c8-81b5-4fbeac003182"), "+359888888888", new Guid("633475e7-ab79-49b2-a198-43e1777e929f") }
                });
        }
    }
}
