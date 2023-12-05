using Features.Clientes;

namespace Features.Testes
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteTesteInvalido : IClassFixture<ClienteTestesFixture>
    {
        private readonly ClienteTestesFixture _clienteTestesFixture;

        public ClienteTesteInvalido(ClienteTestesFixture clienteTestesFixture)
        {
            _clienteTestesFixture = clienteTestesFixture;
        }

        [Fact(DisplayName = "Novo Cliente Invalido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            //Arrange
            var cliente = _clienteTestesFixture.GerarClienteInvalido();

            //Act
            var result = cliente.EhValido();

            //Assert
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
