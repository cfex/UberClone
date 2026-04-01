using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "drivers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    is_verified = table.Column<bool>(type: "boolean", nullable: false),
                    fare_amount = table.Column<double>(type: "double precision", nullable: false),
                    fare_currency = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_documents_drivers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "drivers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    make = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    license_plate = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    registration_date = table.Column<DateTime>(type: "date", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.vehicle_id);
                    table.ForeignKey(
                        name: "FK_vehicles_drivers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "drivers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_documents_driver_id",
                table: "documents",
                column: "driver_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_driver_id",
                table: "vehicles",
                column: "driver_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "drivers");
        }
    }
}
