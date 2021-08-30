using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class _29_08_21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    ReaderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberCC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.ReaderId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReaderId = table.Column<long>(type: "bigint", nullable: true),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Reader_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Reader",
                        principalColumn: "ReaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    FavouriteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReaderId = table.Column<long>(type: "bigint", nullable: true),
                    BookId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.FavouriteId);
                    table.ForeignKey(
                        name: "FK_Favourites_Reader_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Reader",
                        principalColumn: "ReaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ReaderId",
                table: "Bookings",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_ReaderId",
                table: "Favourites",
                column: "ReaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Reader");
        }
    }
}
