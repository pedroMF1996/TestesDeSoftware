using NerdStore.WebApp.MVC;
using NerStore.Integrations.Tests.Config;

namespace NerStore.Integrations.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class UsuarioTests : IClassFixture<IntegrationTestsFixture<Program>>
    {
        private readonly IntegrationTestsFixture<Program> _fixture;

        public UsuarioTests(IntegrationTestsFixture<Program> fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Realizar Cadastro Com Sucesso")]
        [Trait("Categoria", "Integracao Web - Usuario")]
        public async Task Usuario_RealizarCadastro_DeveRrealizarCadastroComSucesso()
        {
            // Arrange
            var initialResponse = await _fixture.HttpClient.GetAsync("/Identity/Account/Register");
            initialResponse.EnsureSuccessStatusCode();

            // Act

            // Assert

        }

    }
}
