using Features.Clientes;
using FluentAssertions;
using Xunit.Abstractions;

namespace Features.Testes
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteMensagemDeSaidaTestes : IClassFixture<ClienteTesteAutoMockerFixture>
    {
        readonly ClienteTesteAutoMockerFixture _clienteTesteAutoMockerFixture;
        readonly ITestOutputHelper _testOutputHelper;

        private readonly ClienteService _clienteService;

        public ClienteMensagemDeSaidaTestes(ClienteTesteAutoMockerFixture clienteBogusFixture, ITestOutputHelper testOutputHelper)
        {
            //Arrange
            _clienteTesteAutoMockerFixture = clienteBogusFixture;
            _clienteService = _clienteTesteAutoMockerFixture.ObterClienteService();
            _testOutputHelper = testOutputHelper;
        }

        [Fact(DisplayName ="Novo Cliente Valido")]
        [Trait("Categoria","Cliente MensagemDeSaida Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            //Arange
            var cliente = _clienteTesteAutoMockerFixture.GerarClienteValido();


            //Act
            var result = cliente.EhValido();

            
            //Assert
            result.Should().BeTrue("Deve ser um cliente valido");
            cliente.ValidationResult.Errors.Should().HaveCount(0, "Nao deve possuir erros de validacao");

        }
        
        [Fact(DisplayName = "Novo Cliente Invalido")]
        [Trait("Categoria", "Cliente MensagemDeSaida Testes")]
        public void Cliente_NovoCliente_DeveEstarInValido()
        {
            //Arange
            var cliente = _clienteTesteAutoMockerFixture.GerarClienteInvalido();


            //Act
            var result = cliente.EhValido();


            //Assert
            result.Should().BeFalse("Deve ser um cliente invalido");
            cliente.ValidationResult.Errors.Should().HaveCountGreaterThan(0, "Deve possuir erros de validacao");
            _testOutputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros nesta validacao");
        }
    }
}
