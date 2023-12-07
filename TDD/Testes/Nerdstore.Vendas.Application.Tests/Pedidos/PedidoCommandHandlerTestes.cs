using MediatR;
using Moq;
using Moq.AutoMock;
using Nerdstore.Vendas.Application.Commands;
using Nerdstore.Vendas.Domain.Entidades;
using Nerdstore.Vendas.Domain.Repositories;

namespace Nerdstore.Vendas.Application.Tests.Pedidos
{
    public class PedidoCommandHandlerTestes
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido Com Sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_NovoPedido_DeveExecutarComSucesso()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(Guid.NewGuid(),Guid.NewGuid(), "Item Teste", 2, 100);
            var mocker = new AutoMocker();

            var commandHadler = mocker.CreateInstance<PedidoCommandHandler>();

            // Act
            var result = await commandHadler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.Adicionar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }
    }
}
