using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDriversLKL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "latitude",
                table: "drivers",
                type: "double precision",
                nullable: true,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "longitude",
                table: "drivers",
                type: "double precision",
                nullable: true,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitude",
                table: "drivers");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "drivers");
        }
    }
}
