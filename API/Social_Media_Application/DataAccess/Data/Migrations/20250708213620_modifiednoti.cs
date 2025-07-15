using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class modifiednoti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userImage",
                table: "notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "dc914efb-ff9e-4ec5-b4f5-bb031d89159e", new DateTime(2025, 7, 9, 0, 36, 17, 804, DateTimeKind.Local).AddTicks(5971), "6b74009e-820a-4901-a5d3-bde473c3407d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "9a658ac9-1bef-403e-8d18-b4dee118bd52", new DateTime(2025, 7, 9, 0, 36, 17, 806, DateTimeKind.Local).AddTicks(9631), "f61234a0-ec42-48bc-8cb8-b3210c4c5e62" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "1812a24b-b0b3-4180-b3e1-2109c0708a3d", new DateTime(2025, 7, 9, 0, 36, 17, 806, DateTimeKind.Local).AddTicks(9664), "949caa64-7e12-4077-a894-029d6ea3d703" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(5337));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(5934));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(5936));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(4166));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(4803));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 21, 36, 17, 807, DateTimeKind.Utc).AddTicks(4805));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "userImage",
                table: "notifications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "8eedbf3c-c42e-4e9a-9841-71446fbacd63", new DateTime(2025, 7, 8, 20, 39, 15, 129, DateTimeKind.Local).AddTicks(7034), "557fe1fc-7815-4c2b-bb61-46fd5ff94998" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "a823ffc2-f631-49c1-9952-f26a093c909a", new DateTime(2025, 7, 8, 20, 39, 15, 133, DateTimeKind.Local).AddTicks(2379), "34c37bb0-a344-41a5-9fce-8a59eaa2ad0b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "ffaaba0b-0379-47de-8a81-7ec8daaf12ca", new DateTime(2025, 7, 8, 20, 39, 15, 133, DateTimeKind.Local).AddTicks(2456), "c008173a-a904-4225-916a-2a629412f665" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 17, 39, 15, 134, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 17, 39, 15, 134, DateTimeKind.Utc).AddTicks(659));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 17, 39, 15, 134, DateTimeKind.Utc).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 17, 39, 15, 133, DateTimeKind.Utc).AddTicks(8660));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 17, 39, 15, 133, DateTimeKind.Utc).AddTicks(9342));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 17, 39, 15, 133, DateTimeKind.Utc).AddTicks(9344));
        }
    }
}
