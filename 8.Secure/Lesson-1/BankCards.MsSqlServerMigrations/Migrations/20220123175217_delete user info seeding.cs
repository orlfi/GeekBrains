using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankCards.DAL.Migrations
{
    public partial class deleteuserinfoseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36774ee0-9c2a-4dfa-9cff-2501f1aae385");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5cfdd5a9-36ff-4b68-859b-f54c7f3be976");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 23, 20, 52, 17, 337, DateTimeKind.Local).AddTicks(3061), new DateTime(2025, 1, 23, 20, 52, 17, 337, DateTimeKind.Local).AddTicks(3067) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 23, 20, 52, 17, 337, DateTimeKind.Local).AddTicks(3074), new DateTime(2025, 1, 23, 20, 52, 17, 337, DateTimeKind.Local).AddTicks(3074) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "ValidThru" },
                values: new object[] { new DateTime(2022, 1, 23, 20, 52, 17, 337, DateTimeKind.Local).AddTicks(3076), new DateTime(2025, 1, 23, 20, 52, 17, 337, DateTimeKind.Local).AddTicks(3076) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
