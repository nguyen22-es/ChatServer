using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMessages_Messages_RoomId",
                table: "RoomMessages");

            migrationBuilder.DropIndex(
                name: "IX_RoomMessages_RoomId",
                table: "RoomMessages");

            migrationBuilder.CreateIndex(
                name: "IX_RoomMessages_MessagesId",
                table: "RoomMessages",
                column: "MessagesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMessages_Messages_MessagesId",
                table: "RoomMessages",
                column: "MessagesId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMessages_Messages_MessagesId",
                table: "RoomMessages");

            migrationBuilder.DropIndex(
                name: "IX_RoomMessages_MessagesId",
                table: "RoomMessages");

            migrationBuilder.CreateIndex(
                name: "IX_RoomMessages_RoomId",
                table: "RoomMessages",
                column: "RoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMessages_Messages_RoomId",
                table: "RoomMessages",
                column: "RoomId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
