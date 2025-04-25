using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class NullableBio : Migration
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
                values: new object[] { "f735a54b-7ad9-4189-bf8d-0aa907006035", "11300c80-cb08-41a8-bc43-e9afabf1d76f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f42107b4-2922-4630-93d8-ebe37c73a45f", "c6056737-0b86-45cb-8e4e-47664639f46c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2375510a-cafc-41e6-be05-56be8eb9d25a", "3952e9e9-72bf-435a-a349-ce8578d6afbd" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 25, 12, 34, 38, 491, DateTimeKind.Utc).AddTicks(6687));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 25, 12, 34, 38, 491, DateTimeKind.Utc).AddTicks(7188));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 25, 12, 34, 38, 491, DateTimeKind.Utc).AddTicks(7189));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "MediaType" },
                values: new object[] { new DateTime(2025, 4, 25, 12, 34, 38, 491, DateTimeKind.Utc).AddTicks(5678), null });

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "MediaType" },
                values: new object[] { new DateTime(2025, 4, 25, 12, 34, 38, 491, DateTimeKind.Utc).AddTicks(6288), null });

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "MediaType" },
                values: new object[] { new DateTime(2025, 4, 25, 12, 34, 38, 491, DateTimeKind.Utc).AddTicks(6289), null });
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
