using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Purchase.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompraCab",
                columns: table => new
                {
                    Id_CompraCab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FecRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraCab", x => x.Id_CompraCab);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoCab",
                columns: table => new
                {
                    Id_MovimientoCab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fec_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_TipoMovimiento = table.Column<int>(type: "int", nullable: false),
                    Id_DocumentoOrigen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoCab", x => x.Id_MovimientoCab);
                });

            migrationBuilder.CreateTable(
                name: "CompraDet",
                columns: table => new
                {
                    Id_CompraDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_CompraCab = table.Column<int>(type: "int", nullable: false),
                    Id_producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Sub_Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraDet", x => x.Id_CompraDet);
                    table.ForeignKey(
                        name: "FK_CompraDet_CompraCab_Id_CompraCab",
                        column: x => x.Id_CompraCab,
                        principalTable: "CompraCab",
                        principalColumn: "Id_CompraCab",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoDet",
                columns: table => new
                {
                    Id_MovimientoDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_movimientocab = table.Column<int>(type: "int", nullable: false),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoDet", x => x.Id_MovimientoDet);
                    table.ForeignKey(
                        name: "FK_MovimientoDet_MovimientoCab_Id_movimientocab",
                        column: x => x.Id_movimientocab,
                        principalTable: "MovimientoCab",
                        principalColumn: "Id_MovimientoCab",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompraDet_Id_CompraCab",
                table: "CompraDet",
                column: "Id_CompraCab");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoDet_Id_movimientocab",
                table: "MovimientoDet",
                column: "Id_movimientocab");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraDet");

            migrationBuilder.DropTable(
                name: "MovimientoDet");

            migrationBuilder.DropTable(
                name: "CompraCab");

            migrationBuilder.DropTable(
                name: "MovimientoCab");
        }
    }
}
