using Nerdstore.Vendas.Domain.Entidades;

namespace Nerdstore.Vendas.Domain.Testes
{
    public class PedidoTestes
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Pedido Testes")]
        public void Adicionar_ItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = new Pedido();
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Livro Caro", 2, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);


            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }
    }
}
