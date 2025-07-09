using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Application.Migrations
{
    /// <inheritdoc />
    public partial class addednotis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_AspNetUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notifications_AspNetUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_notifications_FromUserId",
                table: "notifications",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_ToUserId",
                table: "notifications",
                column: "ToUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.AddColumn<int>(
                name: "UnreadMessages",
                table: "conversations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "8c8241a8-97da-41c4-8433-47bced01c645", new DateTime(2025, 7, 8, 6, 46, 58, 283, DateTimeKind.Local).AddTicks(5717), "55100ba0-766a-4679-a5ec-e2c8acd22f02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "3e277908-8ec1-49b6-ad75-4d991de11924", new DateTime(2025, 7, 8, 6, 46, 58, 286, DateTimeKind.Local).AddTicks(5002), "bce4285d-7bb9-4aef-9116-06b619ba5501" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "JoinedIn", "SecurityStamp" },
                values: new object[] { "639b36ea-02e0-4f76-af4d-6a076d838bdd", new DateTime(2025, 7, 8, 6, 46, 58, 286, DateTimeKind.Local).AddTicks(5054), "e1b4cdac-6989-44f2-892e-357499037370" });

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 3, 46, 58, 287, DateTimeKind.Utc).AddTicks(1332));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 3, 46, 58, 287, DateTimeKind.Utc).AddTicks(1863));

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 3, 46, 58, 287, DateTimeKind.Utc).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 3, 46, 58, 286, DateTimeKind.Utc).AddTicks(9924));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 3, 46, 58, 287, DateTimeKind.Utc).AddTicks(626));

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 3, 46, 58, 287, DateTimeKind.Utc).AddTicks(628));
        }
    }
}
