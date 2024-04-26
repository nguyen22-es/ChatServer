using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "type",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoomsId",
                table: "RoomMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomMessages_RoomsId",
                table: "RoomMessages",
                column: "RoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMessages_Rooms_RoomsId",
                table: "RoomMessages",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMessages_Rooms_RoomsId",
                table: "RoomMessages");

            migrationBuilder.DropIndex(
                name: "IX_RoomMessages_RoomsId",
                table: "RoomMessages");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomsId",
                table: "RoomMessages");
        }
    }
}
