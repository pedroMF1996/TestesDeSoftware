using Nerdstore.Vendas.Domain.Entidades;
using Xunit;
using static Nerdstore.Vendas.Domain.Entidades.Pedido;

namespace Nerdstore.Vendas.Domain.Testes
{
    public class PedidoTestes
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Pedido Testes")]
        public void Adicionar_ItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho();
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Livro Caro", 2, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);


            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }


        [Fact(DisplayName = "Adicionar Item Pedido Existente")]
        [Trait("Categoria", "Pedido Testes")]
        public void AdicionarPedidoItem_ItemExistente_DeveIncrementarUnidadesSomarValor()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho();
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Livro Caro", 2, 100);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItem2 = new PedidoItem(produtoId, "Livro Caro", 1, 100);

            // Act
            pedido.AdicionarItem(pedidoItem2);

            // Assert
            Assert.Equal(300,pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(i => i.Id == pedidoItem.Id).Quantidade);
        }
    }
}
