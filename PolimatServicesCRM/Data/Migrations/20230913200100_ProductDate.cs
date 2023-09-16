using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolimatServicesCRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsServices_Invoices_InvoiceModelInvoiceId",
                table: "ProductsServices");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceModelInvoiceId",
                table: "ProductsServices",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MadeDate",
                table: "ProductsServices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsServices_Invoices_InvoiceModelInvoiceId",
                table: "ProductsServices",
                column: "InvoiceModelInvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsServices_Invoices_InvoiceModelInvoiceId",
                table: "ProductsServices");

            migrationBuilder.DropColumn(
                name: "MadeDate",
                table: "ProductsServices");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceModelInvoiceId",
                table: "ProductsServices",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsServices_Invoices_InvoiceModelInvoiceId",
                table: "ProductsServices",
                column: "InvoiceModelInvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId");
        }
    }
}
