using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class shrinkeditalittle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "posts",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 50000);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "7ceb714f-476d-4117-906e-40e739ed3078", new DateTime(2025, 7, 8, 0, 8, 6, 344, DateTimeKind.Local).AddTicks(1022), "6a3e47a6-1a15-4ef0-aabe-cd3c3bfa780a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "6804ff63-fcbd-459c-835a-2663aa45cdfd", new DateTime(2025, 7, 8, 0, 8, 6, 346, DateTimeKind.Local).AddTicks(3529), "3fc7c434-814f-41c2-a7e1-02629bedc049" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "0fbf1778-46db-445a-8655-508cbb49f136", new DateTime(2025, 7, 8, 0, 8, 6, 346, DateTimeKind.Local).AddTicks(3559), "a6a64a6b-7086-4cf9-b257-79df08f28cf1" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 8, 6, 346, DateTimeKind.Utc).AddTicks(8898));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 8, 6, 346, DateTimeKind.Utc).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 8, 6, 346, DateTimeKind.Utc).AddTicks(9386));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 8, 6, 346, DateTimeKind.Utc).AddTicks(7733));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 8, 6, 346, DateTimeKind.Utc).AddTicks(8397));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 7, 21, 8, 6, 346, DateTimeKind.Utc).AddTicks(8398));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "posts",
                type: "nvarchar(max)",
                maxLength: 50000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

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
    }
}
