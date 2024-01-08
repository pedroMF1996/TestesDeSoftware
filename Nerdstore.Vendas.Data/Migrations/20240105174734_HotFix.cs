using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nerdstore.Vendas.Data.Migrations
{
    /// <inheritdoc />
    public partial class HotFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PedidoId",
                table: "PedidoItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "PedidoItems");
        }
    }
}
