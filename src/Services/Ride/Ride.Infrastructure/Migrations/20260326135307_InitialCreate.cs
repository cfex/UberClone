using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ride.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rides",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    PassengerId = table.Column<Guid>(type: "uuid", nullable: false),
                    pickup_longitude = table.Column<double>(type: "double precision", nullable: false),
                    pickup_latitude = table.Column<double>(type: "double precision", nullable: false),
                    destination_longitude = table.Column<double>(type: "double precision", nullable: false),
                    destination_latitude = table.Column<double>(type: "double precision", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    currency = table.Column<string>(type: "text", nullable: false, defaultValue: "USD"),
                    proposed_price = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    proposed_currency = table.Column<string>(type: "text", nullable: false, defaultValue: "USD"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rides", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rides");
        }
    }
}
