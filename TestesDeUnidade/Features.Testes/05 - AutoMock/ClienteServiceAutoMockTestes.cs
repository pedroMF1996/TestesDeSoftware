using Features.Clientes;
using MediatR;
using Moq;
using Moq.AutoMock;

namespace Features.Testes
{
    public class ClienteServiceAutoMockTestes : IClassFixture<ClienteBogusFixture>
    {
        readonly ClienteBogusFixture _clienteBogusFixture;

        public ClienteServiceAutoMockTestes(ClienteBogusFixture clienteBogusFixture)
        {
            _clienteBogusFixture = clienteBogusFixture;
        }

        [Fact(DisplayName ="Adicionar Cliente com Sucesso")]
        [Trait("Categoria","Cliente Service AutoMock Testes")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arange
            var cliente = _clienteBogusFixture.GerarClienteValido();

            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>();


            //Act
            clienteService.Adicionar(cliente);

            
            //Assert
            Assert.True(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None),Times.Once);
        }
        
        [Fact(DisplayName ="Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMock Testes")]
        public void ClienteService_Adicionar_DeveExecutarInvalido()
        {
            //Arange
            var cliente = _clienteBogusFixture.GerarClienteInvalido();

            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>();


            //Act
            clienteService.Adicionar(cliente);


            //Assert
            Assert.False(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }
        
        [Fact(DisplayName ="Obter Clientes Ativos com Sucesso")]
        [Trait("Categoria","Cliente Service AutoMock Testes")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arange
            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>();

            mocker.GetMock<IClienteRepository>().Setup(r => r.ObterTodos())
                .Returns(_clienteBogusFixture.GerarClientesVariados());

            //Act
            var clientes = clienteService.ObterTodosAtivos().ToList();

            //Assert
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.All(clientes, c => Assert.True(c.Ativo));
        }

    }
}
