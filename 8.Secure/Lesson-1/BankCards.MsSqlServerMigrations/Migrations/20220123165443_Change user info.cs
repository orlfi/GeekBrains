using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankCards.DAL.Migrations
{
    public partial class Changeuserinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52c9b010-87ec-4ba2-adb3-b33884de7d35");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e30918b0-5ef6-40b4-97b4-580453e2578a");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "AspNetUsers",
                newName: "MiddleName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "36774ee0-9c2a-4dfa-9cff-2501f1aae385", 0, "b94c13a4-0b7c-4198-a3ab-2026e6903f3a", "testuserfirst@test.com", false, "Иван", "Иванов", false, null, "Иванович", null, null, null, null, false, "de9ade3d-337f-43ca-9ef0-2fb0161e17e7", false, "First" },
                    { "5cfdd5a9-36ff-4b68-859b-f54c7f3be976", 0, "77c6eb86-867a-4f69-83da-c3675003052b", "testusersecond@test.com", false, "Петр", "Петров", false, null, "Петрович", null, null, null, null, false, "e94df20e-43be-4f2f-87fa-0a2d3dd6b6bd", false, "Second" }
                });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 23, 19, 54, 43, 728, DateTimeKind.Local).AddTicks(3766), new DateTime(2025, 1, 23, 19, 54, 43, 728, DateTimeKind.Local).AddTicks(3775) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 23, 19, 54, 43, 728, DateTimeKind.Local).AddTicks(3780), new DateTime(2025, 1, 23, 19, 54, 43, 728, DateTimeKind.Local).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 23, 19, 54, 43, 728, DateTimeKind.Local).AddTicks(3782), new DateTime(2025, 1, 23, 19, 54, 43, 728, DateTimeKind.Local).AddTicks(3782) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36774ee0-9c2a-4dfa-9cff-2501f1aae385");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5cfdd5a9-36ff-4b68-859b-f54c7f3be976");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "AspNetUsers",
                newName: "DisplayName");

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
    }
}
