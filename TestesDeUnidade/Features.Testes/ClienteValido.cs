using Features.Clientes;

namespace Features.Testes
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteValido : IClassFixture<ClienteTestesFixture>
    {
        private readonly ClienteTestesFixture _clienteTestesFixture;

        public ClienteValido(ClienteTestesFixture clienteTestesFixture)
        {
            _clienteTestesFixture = clienteTestesFixture;
        }

        [Fact(DisplayName = "Novo Cliente Valido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            //Arrange
            var cliente = _clienteTestesFixture.GerarClienteValido();

            //Act
            var result = cliente.EhValido();

            //Assert
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
