using Features.Clientes;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;

namespace Features.Testes
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServicesFluentAssertionTestes : IClassFixture<ClienteTesteAutoMockerFixture>
    {
        readonly ClienteTesteAutoMockerFixture _clienteTesteAutoMockerFixture;
        private readonly ClienteService _clienteService;

        public ClienteServicesFluentAssertionTestes(ClienteTesteAutoMockerFixture clienteBogusFixture)
        {
            //Arrange
            _clienteTesteAutoMockerFixture = clienteBogusFixture;
            _clienteService = _clienteTesteAutoMockerFixture.ObterClienteService();
        }

        [Fact(DisplayName ="Adicionar Cliente com Sucesso")]
        [Trait("Categoria","Cliente Service FluentAssertion Testes")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arange
            var cliente = _clienteTesteAutoMockerFixture.GerarClienteValido();


            //Act
            _clienteService.Adicionar(cliente);


            //Assert
            cliente.EhValido().Should().BeTrue("Deve ser um cliente valido");


            _clienteTesteAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
                .Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTesteAutoMockerFixture.Mocker.GetMock<IMediator>()
                .Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None),Times.Once);
        }
        
        [Fact(DisplayName ="Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service FluentAssertion Testes")]
        public void ClienteService_Adicionar_DeveExecutarInvalido()
        {
            //Arange
            var cliente = _clienteTesteAutoMockerFixture.GerarClienteInvalido();
            

            //Act
            _clienteService.Adicionar(cliente);


            //Assert
            cliente.EhValido().Should().BeFalse("Deve ser um cliente invalido"); 

            _clienteTesteAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
                .Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTesteAutoMockerFixture.Mocker.GetMock<IMediator>()
                .Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }
        
        [Fact(DisplayName ="Obter Clientes Ativos com Sucesso")]
        [Trait("Categoria", "Cliente Service FluentAssertion Testes")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arange
            _clienteTesteAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(r => r.ObterTodos())
                .Returns(_clienteTesteAutoMockerFixture.GerarClientesVariados());


            //Act
            var clientes = _clienteService.ObterTodosAtivos().ToList();


            //Assert
            
            clientes.Should().HaveCountGreaterThan(0);
            clientes.Should().NotContain(c => !c.Ativo);

            _clienteTesteAutoMockerFixture.Mocker.GetMock<IClienteRepository>()
                .Verify(r => r.ObterTodos(), Times.Once);

            _clienteService.ExecutionTimeOf(c => c.ObterTodosAtivos())
                .Should().BeLessThanOrEqualTo(1.Milliseconds());
        }
    }
}
