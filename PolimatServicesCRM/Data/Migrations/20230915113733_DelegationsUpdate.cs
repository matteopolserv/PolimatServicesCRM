using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolimatServicesCRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class DelegationsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DeleagationPerDiemRate",
                table: "Delegations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DistanceDistanceRate",
                table: "Delegations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleagationPerDiemRate",
                table: "Delegations");

            migrationBuilder.DropColumn(
                name: "DistanceDistanceRate",
                table: "Delegations");
        }
    }
}
