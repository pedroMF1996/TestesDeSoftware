using Nerdstore.Core.DomainObjects;
using Nerdstore.Vendas.Domain.Entidades;

using static Nerdstore.Vendas.Domain.Entidades.Pedido;

namespace Nerdstore.Vendas.Domain.Testes
{
    public class PedidoTestes
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
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
        [Trait("Categoria", "Vendas - Pedido")]
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

        [Fact(DisplayName = "Adicionar Item Pedido Acima do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho();
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Livro Caro", MAX_UNIDADES_ITEM + 1, 100);

            // Act &  Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));

        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente Acima do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_EditarQuantidadeItemAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho();
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Livro Caro", MAX_UNIDADES_ITEM, 100);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItem2 = new PedidoItem(produtoId, "Livro Caro", 1, 100);

            // Act &  Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem2));

        }

        [Fact(DisplayName = "Adicionar Item Pedido Acima do Permitido", Skip = "Quantidade minima sendo testada em Vendas - Pedido Item")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemAbaixoDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho();
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Livro Caro", MIN_UNIDADES_ITEM - 1, 100);

            // Act &  Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));

        }
    }
}
