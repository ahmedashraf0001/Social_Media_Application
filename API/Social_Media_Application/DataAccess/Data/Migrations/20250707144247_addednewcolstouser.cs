using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class addednewcolstouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedIn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryPhotoUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "Location", "SecondaryPhotoUrl", "SecurityStamp" },
                values: new object[] { "97164242-a7cd-49fd-bc71-cb942c26c2f1", new DateTime(2025, 7, 7, 17, 42, 45, 298, DateTimeKind.Local).AddTicks(4331), null, null, "782eb867-eec1-47e1-a2a4-d2755a7a85e8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "Location", "SecondaryPhotoUrl", "SecurityStamp" },
                values: new object[] { "7055b81b-4b61-415d-98d5-176b371d46ad", new DateTime(2025, 7, 7, 17, 42, 45, 300, DateTimeKind.Local).AddTicks(7592), null, null, "bce35db9-77c7-4028-a204-c64cf81e84a5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "Location", "SecondaryPhotoUrl", "SecurityStamp" },
                values: new object[] { "585c6f39-c736-4e4f-9b40-32aecd82f666", new DateTime(2025, 7, 7, 17, 42, 45, 300, DateTimeKind.Local).AddTicks(7635), null, null, "37087b6e-7ded-476c-aee4-d8002e28122f" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinedIn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecondaryPhotoUrl",
                table: "AspNetUsers");

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
    }
}
