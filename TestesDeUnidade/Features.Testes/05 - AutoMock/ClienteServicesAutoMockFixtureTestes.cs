using Features.Clientes;
using MediatR;
using Moq;

namespace Features.Testes
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServicesAutoMockFixtureTestes : IClassFixture<ClienteTesteAutoMockerFixture>
    {
        readonly ClienteTesteAutoMockerFixture _clienteTesteAutoMockerFixture;
        private readonly ClienteService _clienteService;

        public ClienteServicesAutoMockFixtureTestes(ClienteTesteAutoMockerFixture clienteBogusFixture)
        {
            //Arrange
            _clienteTesteAutoMockerFixture = clienteBogusFixture;
            _clienteService = _clienteTesteAutoMockerFixture.ObterClienteService();
        }

        [Fact(DisplayName ="Adicionar Cliente com Sucesso")]
        [Trait("Categoria","Cliente Service AutoMockFixture Testes")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arange
            var cliente = _clienteTesteAutoMockerFixture.GerarClienteValido();


            //Act
            _clienteService.Adicionar(cliente);

            
            //Assert
            Assert.True(cliente.EhValido());
            _clienteTesteAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
                .Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTesteAutoMockerFixture.Mocker.GetMock<IMediator>()
                .Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None),Times.Once);
        }
        
        [Fact(DisplayName ="Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Testes")]
        public void ClienteService_Adicionar_DeveExecutarInvalido()
        {
            //Arange
            var cliente = _clienteTesteAutoMockerFixture.GerarClienteInvalido();
            

            //Act
            _clienteService.Adicionar(cliente);


            //Assert
            Assert.False(cliente.EhValido());
            _clienteTesteAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
                .Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTesteAutoMockerFixture.Mocker.GetMock<IMediator>()
                .Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }
        
        [Fact(DisplayName ="Obter Clientes Ativos com Sucesso")]
        [Trait("Categoria","Cliente Service AutoMockFixture Testes")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arange
            _clienteTesteAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(r => r.ObterTodos())
                .Returns(_clienteTesteAutoMockerFixture.GerarClientesVariados());


            //Act
            var clientes = _clienteService.ObterTodosAtivos().ToList();


            //Assert
            _clienteTesteAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
                .Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.All(clientes, c => Assert.True(c.Ativo));
        }
    }
}
