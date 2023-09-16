using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolimatServicesCRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class Delegations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delegations",
                columns: table => new
                {
                    DelegationId = table.Column<string>(type: "TEXT", nullable: false),
                    DelegatedId = table.Column<string>(type: "TEXT", nullable: false),
                    DelegatedName = table.Column<string>(type: "TEXT", nullable: false),
                    DelegatedPost = table.Column<string>(type: "TEXT", nullable: false),
                    DelegatingId = table.Column<string>(type: "TEXT", nullable: false),
                    DelegatingName = table.Column<string>(type: "TEXT", nullable: false),
                    DelegatingPost = table.Column<string>(type: "TEXT", nullable: false),
                    Vechicle = table.Column<string>(type: "TEXT", nullable: false),
                    DelegationStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DelegationEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeleagationDeparturePlace = table.Column<string>(type: "TEXT", nullable: false),
                    DeleagationArrivngPlace = table.Column<string>(type: "TEXT", nullable: false),
                    DelegationPurpose = table.Column<string>(type: "TEXT", nullable: false),
                    Distance = table.Column<decimal>(type: "TEXT", nullable: false),
                    DeleagationDuringTime = table.Column<int>(type: "INTEGER", nullable: false),
                    DeleagationPerDiemTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    DelegationOvernightStay = table.Column<decimal>(type: "TEXT", nullable: false),
                    DistanceTotalCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    DelegationTotalCost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delegations", x => x.DelegationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delegations");
        }
    }
}
