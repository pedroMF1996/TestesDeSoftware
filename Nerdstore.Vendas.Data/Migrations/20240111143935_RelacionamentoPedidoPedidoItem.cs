using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nerdstore.Vendas.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoPedidoPedidoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Pedidos_Id",
                table: "PedidoItems");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItems_PedidoId",
                table: "PedidoItems",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Pedidos_PedidoId",
                table: "PedidoItems",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Pedidos_PedidoId",
                table: "PedidoItems");

            migrationBuilder.DropIndex(
                name: "IX_PedidoItems_PedidoId",
                table: "PedidoItems");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Pedidos_Id",
                table: "PedidoItems",
                column: "Id",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }
    }
}
