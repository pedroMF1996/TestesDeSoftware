using Nerdstore.Core.DomainObjects;
using Nerdstore.Vendas.Domain.Entidades;

namespace Nerdstore.Vendas.Domain.Testes
{
    public class PedidoItemTestes
    {
        [Fact(DisplayName = "Novo Item Pedido Abaixo do Permitodo")]
        [Trait("Categoria", "Vendas - Pedido Item")]
        public void AdicionarPedidoItem_UnidadesItemAbaixoDoPermitido_DeveRetornarException()
        {
            // Arrange
            // Act
            // Assert

            Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(), "Livro Caro", Pedido.MIN_UNIDADES_ITEM - 1, 100));

        }
    }
}
