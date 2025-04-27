using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastMessageContent",
                table: "conversations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e8f02336-09dd-43bd-9e10-a7747be66fbc", "f7bd8ac2-1d34-4267-ad35-2413d816d967" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7e5e55ff-f8d2-4b58-93ad-d2e697050c30", "4b061b5e-4eda-4ffa-a7c7-8e7c2301fc17" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ea103be5-f456-447d-8a78-45cfe515aa75", "dbf744e8-3204-443c-bf0a-63abe0bc3ba1" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 1, 56, 53, 497, DateTimeKind.Utc).AddTicks(8500));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 1, 56, 53, 497, DateTimeKind.Utc).AddTicks(9032));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 1, 56, 53, 497, DateTimeKind.Utc).AddTicks(9033));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 1, 56, 53, 497, DateTimeKind.Utc).AddTicks(7425));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 1, 56, 53, 497, DateTimeKind.Utc).AddTicks(7996));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 1, 56, 53, 497, DateTimeKind.Utc).AddTicks(7998));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMessageContent",
                table: "conversations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "399ac7f7-1500-4340-ad85-56559b3bbdfa", "fc2661d3-b1e2-4450-a286-f86a7535f11e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c99dab15-b975-45bd-b628-a545b3d20a4a", "6ade2f69-02e1-4ab9-a6a5-653727471588" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5f1ba3eb-7ef0-440a-8b05-7267ff52b1f5", "b5fabf73-4276-439b-ab32-69df4cf8b3ce" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 0, 14, 48, 685, DateTimeKind.Utc).AddTicks(361));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 0, 14, 48, 685, DateTimeKind.Utc).AddTicks(836));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 0, 14, 48, 685, DateTimeKind.Utc).AddTicks(837));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 0, 14, 48, 684, DateTimeKind.Utc).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 0, 14, 48, 684, DateTimeKind.Utc).AddTicks(9953));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 27, 0, 14, 48, 684, DateTimeKind.Utc).AddTicks(9954));
        }
    }
}
