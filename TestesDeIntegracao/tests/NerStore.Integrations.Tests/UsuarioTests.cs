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
            var initialResponse = await _fixture.HttpClient.GetAsync(urlRegister);
            initialResponse.EnsureSuccessStatusCode();
            var antiForgeryToken = _fixture.ObiterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>()
            {
                { "Input.Email", _fixture.UsuarioEmail},
                { "Input.Password", _fixture.UsuarioSenha},
                { "Input.ConfirmPassword", _fixture.UsuarioConfirmarSenha},
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
            responseString.Should().Contain($"Hello {_fixture.UsuarioEmail}!");
        }
        
        [Fact(DisplayName = "Nao Realizar Cadastro Com Sucesso De Usuario Com Senha Fraca")]
        [Trait("Categoria", "Integracao Web - Usuario")]
        public async Task UsuarioComSenhaFraca_RealizarCadastro_NaoDeveRrealizarCadastroComSucesso()
        {
            // Arrange
            var urlRegister = "/Identity/Account/Register";
            var initialResponse = await _fixture.HttpClient.GetAsync(urlRegister);
            initialResponse.EnsureSuccessStatusCode();
            var antiForgeryToken = _fixture.ObiterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>()
            {
                { "Input.Email", _fixture.UsuarioEmail},
                { "Input.Password", _fixture.UsuarioSenhaFraca},
                { "Input.ConfirmPassword", _fixture.UsuarioConfirmarSenhaFraca},
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
            responseString.Should().NotContain($"Hello {_fixture.UsuarioEmail}!");
            responseString.Should().Contain("Passwords must have at least one non alphanumeric character.");
            responseString.Should().Contain("Passwords must have at least one lowercase (&#x27;a&#x27;-&#x27;z&#x27;).");
            responseString.Should().Contain("Passwords must have at least one uppercase (&#x27;A&#x27;-&#x27;Z&#x27;).");
        }

    }
}
