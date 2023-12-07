using Nerdstore.Vendas.Application.Commands;
using Nerdstore.Vendas.Domain.Entidades;

namespace Nerdstore.Vendas.Application.Tests.Pedidos
{
    public class AdicionarItemPedidoCommandTestes
    {
        [Fact(DisplayName = "AdicionarItemPedidoCommand Valido")]
        [Trait("Categoria", "Venda - PedidoCommand")]
        public void AdicionarItemPedidoCommand_CommandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);
        }
        
        [Fact(DisplayName = "AdicionarItemPedidoCommand Invalido")]
        [Trait("Categoria", "Venda - PedidoCommand")]
        public void AdicionarItemPedidoCommand_CommandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(Guid.Empty, Guid.Empty, "", 0, 0);

            // Act
            var result = command.EhValido();

            // Assert
            Assert.False(result);
            var errors = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.Contains(AdicionarItemPedidoCommandValidation.IdClienteErroMsg, errors);
            Assert.Contains(AdicionarItemPedidoCommandValidation.IdProdutoErroMsg, errors);
            Assert.Contains(AdicionarItemPedidoCommandValidation.NomeErroMsg, errors);
            Assert.Contains(AdicionarItemPedidoCommandValidation.ValorErroMsg, errors);
            Assert.Contains(AdicionarItemPedidoCommandValidation.QtdMinErroMsg, errors);
        }
        
        [Fact(DisplayName = "AdicionarItemPedidoCommand Invalido QuantidadeSuperioAoDefinido")]
        [Trait("Categoria", "Venda - PedidoCommand")]
        public void AdicionarItemPedidoCommand_QuantidadeSuperioAoDefinido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", Pedido.MAX_UNIDADES_ITEM+1, 100);

            // Act
            var result = command.EhValido();

            // Assert
            Assert.False(result);
            var errors = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.Contains(AdicionarItemPedidoCommandValidation.QtdMaxErroMsg, errors);
        }
    }
}
