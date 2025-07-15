using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class addededitdeleteattribstomessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "19810dbe-c8f3-41d5-a986-44efd6f7d803", new DateTime(2025, 7, 8, 5, 15, 20, 812, DateTimeKind.Local).AddTicks(5640), "a668d4d0-751c-4e9f-8a15-5dbaae425b8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "8206256b-0d7e-4224-8a20-905e78c07aa2", new DateTime(2025, 7, 8, 5, 15, 20, 815, DateTimeKind.Local).AddTicks(1211), "351c1850-3bd4-4f69-ad74-9a44e5b059fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "3250e796-3b32-4bc0-b2ac-6494120513cb", new DateTime(2025, 7, 8, 5, 15, 20, 815, DateTimeKind.Local).AddTicks(1242), "2d3a10ca-2b24-4dab-bce3-0ee2a05ec3a1" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 2, 15, 20, 815, DateTimeKind.Utc).AddTicks(6948));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 2, 15, 20, 815, DateTimeKind.Utc).AddTicks(8041));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 2, 15, 20, 815, DateTimeKind.Utc).AddTicks(8043));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 2, 15, 20, 815, DateTimeKind.Utc).AddTicks(5788));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 2, 15, 20, 815, DateTimeKind.Utc).AddTicks(6373));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 2, 15, 20, 815, DateTimeKind.Utc).AddTicks(6375));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "messages");

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
    }
}
