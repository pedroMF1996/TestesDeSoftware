using FluentAssertions;
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
            var urlRegister = "/Identity/Account/Register";
            var email = "teste@teste.com";
            var initialResponse = await _fixture.HttpClient.GetAsync(urlRegister);
            initialResponse.EnsureSuccessStatusCode();
            var antiForgeryToken = _fixture.ObiterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>()
            {
                { "Input.Email", email},
                { "Input.Password", "Teste@123"},
                { "Input.ConfirmPassword", "Teste@123"},
                { _fixture.AntiForgeryFieldName, antiForgeryToken}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, urlRegister)
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _fixture.HttpClient.SendAsync(postRequest);

            // Assert
            var responseString = await postResponse.Content.ReadAsStringAsync();
            postResponse.EnsureSuccessStatusCode();
            responseString.Should().Contain($"Hello {email}!");
        }

    }
}
