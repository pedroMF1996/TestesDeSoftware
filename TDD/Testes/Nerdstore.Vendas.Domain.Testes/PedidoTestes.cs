using Nerdstore.Core.DomainObjects;
using Nerdstore.Vendas.Domain.Entidades;
using Xunit;

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
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
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
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Livro Caro", 2, 100);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItem2 = new PedidoItem(produtoId, "Livro Caro", 1, 100);

            // Act
            pedido.AdicionarItem(pedidoItem2);

            // Assert
            Assert.Equal(300,pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(i => i.ProdutoId == pedidoItem.ProdutoId).Quantidade);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Acima do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
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
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
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
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Livro Caro", MIN_UNIDADES_ITEM - 1, 100);

            // Act &  Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));

        }

        [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItemAtualizado = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 1000);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Quantidade alem do permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemQuantidadeAlemDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItemId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(pedidoItemId, "Produto Teste", 1, 1000);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItemAtualizado = new PedidoItem(pedidoItemId, "Produto Teste", MAX_UNIDADES_ITEM + 1, 1000);

            // Act & Assert
            var error = Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
            Assert.Equal($"Maximo de {MAX_UNIDADES_ITEM} unidades por produto", error.Message);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Valido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItemId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(pedidoItemId, "Produto Teste", 1, 1000);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItemAtualizado = new PedidoItem(pedidoItemId, "Produto Teste", 1, 2000);

            // Act 
            pedido.AtualizarItem(pedidoItemAtualizado);

            // Assert
            Assert.Equal(2000, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Validar Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_PedidoComProdutosDiferentes_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItemId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(pedidoItemId, "Produto Teste", 1, 1000);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 3, 500);
            pedido.AdicionarItem(pedidoItem);
            pedido.AdicionarItem(pedidoItem2);

            var pedidoItemAtualizado = new PedidoItem(pedidoItemId, "Produto Teste", 1, 2000);

            // Act 
            pedido.AtualizarItem(pedidoItemAtualizado);

            // Assert
            Assert.Equal(3500, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Remover Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemNaoEstaNaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 1000);

            // Act && Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItemRemover));
        }
        
        [Fact(DisplayName = "Remover Item Pedido Existente Atualizar ValorTotal")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemEstaNaLista_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            
            var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 2000);
            pedido.AdicionarItem(pedidoItemRemover);

            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 1000);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 3, 500);
            pedido.AdicionarItem(pedidoItem);
            pedido.AdicionarItem(pedidoItem2);

            // Act 
            pedido.RemoverItem(pedidoItemRemover);

            // Assert
            Assert.Equal(2500, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Valido No Pedido Sem Erros")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Pedido_AplicarVoucherValido_DeveRetornarSemEerros()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("PROMO-15-REAIS", 15, 0, TipoDesconto.Valor, 1, DateTime.Now.AddDays(1), true, false);

            // Act
            var result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Voucher Tipo Valor Desconto Deve Descontar Do Valor Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_VoucherTipoValorDesconto_DeveDescontarDoValorTotal()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 2000);
            pedido.AdicionarItem(pedidoItemRemover);

            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 1000);
            pedido.AdicionarItem(pedidoItem);
            
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 3, 500);
            pedido.AdicionarItem(pedidoItem2);

            var voucher = new Voucher("PROMO-15-REAIS", 15, 0, TipoDesconto.Valor, 1, DateTime.Now.AddDays(1), true, false);

            var valorComDesconto = pedido.ValorTotal - voucher.ValorDesconto;

            // Act
            var result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(valorComDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Voucher Tipo Percentual Desconto Deve Descontar Do Valor Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_VoucherTipoPercentualDesconto_DeveDescontarDoValorTotal()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 2000);
            pedido.AdicionarItem(pedidoItemRemover);

            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 1000);
            pedido.AdicionarItem(pedidoItem);
            
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 3, 500);
            pedido.AdicionarItem(pedidoItem2);

            var voucher = new Voucher("PROMO-15-OFF", 0, 15, TipoDesconto.Porcentagem, 1, DateTime.Now.AddDays(1), true, false);

            var valorComDesconto = pedido.ValorTotal * ( 1 - voucher.PercentualDesconto/100 );

            // Act
            var result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(valorComDesconto, pedido.ValorTotal);
        }
        
        [Fact(DisplayName = "Desconto Excede Valor Total Pedido Deve Zerar Valor Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_DescontoExcedeValorTotalPedido_DeveZerarValorTotal()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 2000);
            pedido.AdicionarItem(pedidoItemRemover);

            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 1000);
            pedido.AdicionarItem(pedidoItem);
            
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 3, 500);
            pedido.AdicionarItem(pedidoItem2);

            var voucher = new Voucher("PROMO-5000-REAIS", 5000, 0, TipoDesconto.Valor, 1, DateTime.Now.AddDays(1), true, false);

            // Act
            var result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(0, pedido.ValorTotal);
        }
        
        [Fact(DisplayName = "Ao Modificar Itens Pedido Voucher Aplicado Deve Descontar Do Valor Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_ModificarItensPedido_DeveDescontarDoValorTotal()
        {
            // Arrange
            var pedido = PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 2000);
            pedido.AdicionarItem(pedidoItemRemover);
            var pedidoItemId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(pedidoItemId, "Produto Teste", 1, 1000);
            pedido.AdicionarItem(pedidoItem);
            
            var pedidoItem2 = new PedidoItem(pedidoItemId, "Produto Teste", 3, 1000);

            var voucher = new Voucher("PROMO-50-REAIS", 50, 0, TipoDesconto.Valor, 1, DateTime.Now.AddDays(1), true, false);
            var result = pedido.AplicarVoucher(voucher);

            
            // Act
            pedido.AdicionarItem(pedidoItem2);

            // Assert
            var valorComDesconto = pedido.PedidoItems.Sum(i => i.CalcularValor()) - voucher.ValorDesconto;
            Assert.True(result.IsValid);
            Assert.Equal(valorComDesconto, pedido.ValorTotal);
        }
    }
}
