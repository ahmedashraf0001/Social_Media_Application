using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class updatedconversationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_conversations_AspNetUsers_User1Id",
                table: "conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_conversations_AspNetUsers_User2Id",
                table: "conversations");

            migrationBuilder.RenameColumn(
                name: "User2Id",
                table: "conversations",
                newName: "otherUserId");

            migrationBuilder.RenameColumn(
                name: "User1Id",
                table: "conversations",
                newName: "CurrentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_conversations_User2Id",
                table: "conversations",
                newName: "IX_conversations_otherUserId");

            migrationBuilder.RenameIndex(
                name: "IX_conversations_User1Id",
                table: "conversations",
                newName: "IX_conversations_CurrentUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_conversations_AspNetUsers_CurrentUserId",
                table: "conversations",
                column: "CurrentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_conversations_AspNetUsers_otherUserId",
                table: "conversations",
                column: "otherUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_conversations_AspNetUsers_CurrentUserId",
                table: "conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_conversations_AspNetUsers_otherUserId",
                table: "conversations");

            migrationBuilder.RenameColumn(
                name: "otherUserId",
                table: "conversations",
                newName: "User2Id");

            migrationBuilder.RenameColumn(
                name: "CurrentUserId",
                table: "conversations",
                newName: "User1Id");

            migrationBuilder.RenameIndex(
                name: "IX_conversations_otherUserId",
                table: "conversations",
                newName: "IX_conversations_User2Id");

            migrationBuilder.RenameIndex(
                name: "IX_conversations_CurrentUserId",
                table: "conversations",
                newName: "IX_conversations_User1Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_conversations_AspNetUsers_User1Id",
                table: "conversations",
                column: "User1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_conversations_AspNetUsers_User2Id",
                table: "conversations",
                column: "User2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
