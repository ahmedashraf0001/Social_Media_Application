using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class expandedcontentsize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "posts",
                type: "nvarchar(max)",
                maxLength: 50000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "8be7f053-79fd-41ca-b3ad-4866f07dd89e", new DateTime(2025, 7, 8, 0, 3, 44, 327, DateTimeKind.Local).AddTicks(2640), "a238e0fc-0be6-4f82-90f0-2122f904bdfa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "fbe19140-b9ee-4c53-ad25-9f4943f84dd7", new DateTime(2025, 7, 8, 0, 3, 44, 330, DateTimeKind.Local).AddTicks(2609), "b3b8a321-7684-4cb1-9224-b4afddd5b7b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "3f4c5f51-21fa-44f3-9c4c-65ac3521ab89", new DateTime(2025, 7, 8, 0, 3, 44, 330, DateTimeKind.Local).AddTicks(2649), "913441e3-0343-4e62-a61c-40cc4434765a" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 3, 44, 330, DateTimeKind.Utc).AddTicks(8928));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 3, 44, 330, DateTimeKind.Utc).AddTicks(9423));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 3, 44, 330, DateTimeKind.Utc).AddTicks(9424));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 3, 44, 330, DateTimeKind.Utc).AddTicks(7824));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 3, 44, 330, DateTimeKind.Utc).AddTicks(8402));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 3, 44, 330, DateTimeKind.Utc).AddTicks(8403));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "posts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 50000);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "97164242-a7cd-49fd-bc71-cb942c26c2f1", new DateTime(2025, 7, 7, 17, 42, 45, 298, DateTimeKind.Local).AddTicks(4331), "782eb867-eec1-47e1-a2a4-d2755a7a85e8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "7055b81b-4b61-415d-98d5-176b371d46ad", new DateTime(2025, 7, 7, 17, 42, 45, 300, DateTimeKind.Local).AddTicks(7592), "bce35db9-77c7-4028-a204-c64cf81e84a5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "585c6f39-c736-4e4f-9b40-32aecd82f666", new DateTime(2025, 7, 7, 17, 42, 45, 300, DateTimeKind.Local).AddTicks(7635), "37087b6e-7ded-476c-aee4-d8002e28122f" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 14, 42, 45, 301, DateTimeKind.Utc).AddTicks(3332));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 14, 42, 45, 301, DateTimeKind.Utc).AddTicks(3841));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 14, 42, 45, 301, DateTimeKind.Utc).AddTicks(3843));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 14, 42, 45, 301, DateTimeKind.Utc).AddTicks(2234));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 14, 42, 45, 301, DateTimeKind.Utc).AddTicks(2839));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 14, 42, 45, 301, DateTimeKind.Utc).AddTicks(2840));
        }
    }
}
