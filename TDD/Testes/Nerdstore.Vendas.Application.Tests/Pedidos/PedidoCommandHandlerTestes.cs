namespace Nerdstore.Vendas.Application.Tests.Pedidos
{
    public class PedidoCommandHandlerTestes
    {
        private readonly AutoMocker mocker;
        private readonly Guid _clienteId;
        private readonly Guid _produtoId;
        private readonly Pedido _pedido;
        private readonly PedidoCommandHandler commandHandler;
        private readonly Mock<IPedidoRepository> pedidoRepository;
        public PedidoCommandHandlerTestes()
        {
            // Arrange
            mocker = new AutoMocker();
            _clienteId = Guid.NewGuid();
            _produtoId = Guid.NewGuid();
            _pedido = Pedido.PedidoFactory.NovoPedidoRascunho(_clienteId);
            commandHandler = mocker.CreateInstance<PedidoCommandHandler>();
            pedidoRepository = mocker.GetMock<IPedidoRepository>();
            pedidoRepository.Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
        }

        [Fact(DisplayName = "Adicionar Item Novo Pedido Com Sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_NovoPedido_DeveExecutarComSucesso()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(_clienteId, _produtoId, "Item Teste", 2, 100);
            
            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            pedidoRepository.Verify(r => r.Adicionar(It.IsAny<Pedido>()), Times.Once);
            pedidoRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        
        [Fact(DisplayName = "Adicionar Novo Item Ao Pedido Rascunho Com Sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_NovoItemAoPedidoRascunho_DeveExecutarComSucesso()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(_clienteId, _produtoId, "Item Teste", 2, 100);
            pedidoRepository.Setup(r => r.ObterPedidoRascunhoPorClienteId(command.ClienteId)).Returns(Task.FromResult(_pedido));

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            pedidoRepository.Verify(r => r.ObterPedidoRascunhoPorClienteId(It.IsAny<Guid>()), Times.Once);
            pedidoRepository.Verify(r => r.AdicionarItem(It.IsAny<PedidoItem>()), Times.Once);
            pedidoRepository.Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            pedidoRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Item Existente Ao Pedido Rascunho Com Sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_ItemExistenteAoPedidoRascunho_DeveExecutarComSucesso()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(_clienteId, _produtoId, "Item Teste", 2, 100);
            
            _pedido.AdicionarItem(new(_produtoId, command.NomePedidoItem, command.QuantidadePedidoItem, command.ValorUnitarioPedidoItem));
            
            pedidoRepository.Setup(r => r.ObterPedidoRascunhoPorClienteId(command.ClienteId)).Returns(Task.FromResult(_pedido));

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            pedidoRepository.Verify(r => r.ObterPedidoRascunhoPorClienteId(It.IsAny<Guid>()), Times.Once);
            pedidoRepository.Verify(r => r.AtualizarItem(It.IsAny<PedidoItem>()), Times.Once);
            pedidoRepository.Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            pedidoRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "AdicionarItem CommandInvalido Deve Retornar Falso E Lancar Eventos De Notificacao")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_CommandInvalido_DeveRetornatFalsoELancarEventosDeNotificacao()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(Guid.Empty, Guid.Empty, string.Empty, 0, 0);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(5));
        }
    }
}
