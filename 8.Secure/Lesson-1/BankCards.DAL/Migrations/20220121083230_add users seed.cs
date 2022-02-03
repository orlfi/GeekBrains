using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankCards.DAL.Migrations
{
    public partial class addusersseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "52c9b010-87ec-4ba2-adb3-b33884de7d35", 0, "568c8314-b24c-46df-be36-fe2c0f5e5932", "TestUserFirst", "testuserfirst@test.com", false, false, null, null, null, null, null, false, "d5da0614-00b1-4e04-a37e-e6f962d4f69c", false, "TestUserFirst" },
                    { "e30918b0-5ef6-40b4-97b4-580453e2578a", 0, "29f312e2-f479-48b4-8b42-054f02e70f33", "TestUserSecond", "testusersecond@test.com", false, false, null, null, null, null, null, false, "caab7b3a-59bf-4a75-bf8e-b1b370823d55", false, "TestUserSecond" }
                });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 21, 11, 32, 30, 457, DateTimeKind.Local).AddTicks(5155), new DateTime(2025, 1, 21, 11, 32, 30, 457, DateTimeKind.Local).AddTicks(5174) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 21, 11, 32, 30, 457, DateTimeKind.Local).AddTicks(5179), new DateTime(2025, 1, 21, 11, 32, 30, 457, DateTimeKind.Local).AddTicks(5179) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 21, 11, 32, 30, 457, DateTimeKind.Local).AddTicks(5183), new DateTime(2025, 1, 21, 11, 32, 30, 457, DateTimeKind.Local).AddTicks(5183) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52c9b010-87ec-4ba2-adb3-b33884de7d35");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e30918b0-5ef6-40b4-97b4-580453e2578a");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 20, 16, 23, 47, 255, DateTimeKind.Local).AddTicks(9591), new DateTime(2025, 1, 20, 16, 23, 47, 255, DateTimeKind.Local).AddTicks(9605) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 20, 16, 23, 47, 255, DateTimeKind.Local).AddTicks(9614), new DateTime(2025, 1, 20, 16, 23, 47, 255, DateTimeKind.Local).AddTicks(9614) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 20, 16, 23, 47, 255, DateTimeKind.Local).AddTicks(9614), new DateTime(2025, 1, 20, 16, 23, 47, 255, DateTimeKind.Local).AddTicks(9614) });
        }
    }
}
