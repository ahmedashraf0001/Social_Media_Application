using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class updatedconversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConversationName",
                table: "conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoURL",
                table: "conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "408be698-e605-4b3d-89c3-7d9d969a0dce", "3d7c17d3-2201-4e3b-8e8d-23cbfff9ef47" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8f118f6c-67b0-4978-95d6-7ef0a5ea0559", "2bb609ea-264f-46e7-b732-f45ba350676f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e90194e0-b5be-4cda-83e7-38891a04bec3", "a5f68633-1f52-4896-9de1-91d103c409a7" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 20, 54, 22, 150, DateTimeKind.Utc).AddTicks(9424));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 20, 54, 22, 150, DateTimeKind.Utc).AddTicks(9882));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 20, 54, 22, 150, DateTimeKind.Utc).AddTicks(9884));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 20, 54, 22, 150, DateTimeKind.Utc).AddTicks(8435));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 20, 54, 22, 150, DateTimeKind.Utc).AddTicks(9021));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 20, 54, 22, 150, DateTimeKind.Utc).AddTicks(9022));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversationName",
                table: "conversations");

            migrationBuilder.DropColumn(
                name: "PhotoURL",
                table: "conversations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f72a021c-f45d-402f-a922-ed64def60332", "7cd51352-d8ec-4310-bfbb-e49a5fb08cbf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "85ec3d14-791a-40c7-910e-b46ecc9fe7fd", "1db5d395-a067-4dc9-92f7-192503d7bdcb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dadcbaa2-a422-4644-b908-f7c6cd7b529f", "e6c91b7d-c7e7-4b52-b756-f8af3334a914" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 19, 41, 30, 736, DateTimeKind.Utc).AddTicks(9349));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 19, 41, 30, 736, DateTimeKind.Utc).AddTicks(9970));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 19, 41, 30, 736, DateTimeKind.Utc).AddTicks(9972));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 19, 41, 30, 736, DateTimeKind.Utc).AddTicks(8024));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 19, 41, 30, 736, DateTimeKind.Utc).AddTicks(8765));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 4, 28, 19, 41, 30, 736, DateTimeKind.Utc).AddTicks(8768));
        }
    }
}
