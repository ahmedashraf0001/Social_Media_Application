using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class updatedConvo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "4356dc92-280e-47a1-9898-45d078b3ef0b", "c2ebf4dd-9c90-4660-b01b-e2c2e02dbf29" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4cf0cdc9-e56f-4627-9cc8-08bee3bda0a1", "485d7b1a-97ac-4043-a5e5-7cf4704538a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e086ba2d-d158-402c-999c-9e8044d2e1a4", "db5af5c8-ae27-48b7-9db3-acec32671a52" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 2, 20, 41, 49, 169, DateTimeKind.Utc).AddTicks(1047));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 2, 20, 41, 49, 169, DateTimeKind.Utc).AddTicks(1674));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 2, 20, 41, 49, 169, DateTimeKind.Utc).AddTicks(1675));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 2, 20, 41, 49, 168, DateTimeKind.Utc).AddTicks(9880));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 2, 20, 41, 49, 169, DateTimeKind.Utc).AddTicks(527));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 2, 20, 41, 49, 169, DateTimeKind.Utc).AddTicks(529));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
