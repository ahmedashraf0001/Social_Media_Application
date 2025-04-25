using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class addedpostsandfeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "posts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "00c42011-ba8c-481d-8c76-441f76170123", "2d17db96-952d-4b84-811d-f55d6bc72105" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0988ec96-0822-4ed3-82f0-0ae2c3881f85", "83da4e8d-e86c-4af5-97b7-ccc5c76e8a3a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1930c9b2-37c0-40d4-b18c-0bb7a51d980b", "6ab63bd4-a576-4891-a30d-37560386e7cd" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 25, 19, 16, 22, 949, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 25, 19, 16, 22, 949, DateTimeKind.Utc).AddTicks(1905));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 25, 19, 16, 22, 949, DateTimeKind.Utc).AddTicks(1907));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "MediaType" },
                values: new object[] { new DateTime(2025, 4, 25, 19, 16, 22, 948, DateTimeKind.Utc).AddTicks(9854), null });

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "MediaType" },
                values: new object[] { new DateTime(2025, 4, 25, 19, 16, 22, 949, DateTimeKind.Utc).AddTicks(624), null });

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "MediaType" },
                values: new object[] { new DateTime(2025, 4, 25, 19, 16, 22, 949, DateTimeKind.Utc).AddTicks(625), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "posts");

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7436d681-4d55-4693-aa19-ff4c7222958b", "6625c37d-ea83-4f95-aad5-62b78e13b279" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4576004a-d62c-4aa3-a408-760ad2f13643", "f36d3ccf-4b5d-489c-b111-7e954f083d45" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b8c503ba-4dc1-433e-88f4-852b9f682909", "dad6c1fb-b205-42c2-9b13-55204bd582eb" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 24, 16, 34, 46, 777, DateTimeKind.Utc).AddTicks(5145));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 24, 16, 34, 46, 777, DateTimeKind.Utc).AddTicks(5718));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 24, 16, 34, 46, 777, DateTimeKind.Utc).AddTicks(5719));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 24, 16, 34, 46, 777, DateTimeKind.Utc).AddTicks(3828));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 24, 16, 34, 46, 777, DateTimeKind.Utc).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 24, 16, 34, 46, 777, DateTimeKind.Utc).AddTicks(4420));
        }
    }
}
