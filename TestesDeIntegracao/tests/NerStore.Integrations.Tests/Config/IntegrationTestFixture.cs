using Bogus;
using Features.Clientes;
using Microsoft.AspNetCore.Mvc.Testing;
using NerdStore.WebApp.MVC;
using NerdStore.WebApp.MVC.Models;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace NerStore.Integrations.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>>
    { }
    
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>>
    { }

    public class IntegrationTestsFixture<TProgram> : IDisposable where TProgram : class
    {

        public readonly LojaAppFacory<TProgram> Factory;
        public HttpClient HttpClient;
        public readonly string AntiForgeryFieldName = "__RequestVerificationToken";
        public string UsuarioEmail;
        public string UsuarioSenha;
        public readonly string UsuarioConfirmarSenha;
        public readonly string UsuarioSenhaFraca;
        public readonly string UsuarioConfirmarSenhaFraca;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7,
            };

            Factory = new LojaAppFacory<TProgram>();
            HttpClient = Factory.CreateClient(clientOptions);
            var faker = new Faker("pt_BR");

            #region Credenciais

            UsuarioEmail = faker.Internet.Email();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
            UsuarioConfirmarSenha = UsuarioSenha;
            
            UsuarioSenhaFraca = "12345678";
            UsuarioConfirmarSenhaFraca = UsuarioSenhaFraca;

            #endregion

        }

        public string ObterAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch =
                Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;

            throw new ArgumentException($"AntiForgeryToken {AntiForgeryFieldName} nao encontrado no HTML {htmlBody}");
        }

        public async Task RealizarRegistrarUsuarioWeb()
        {
            var urlRegister = "/Identity/Account/Register";
            var initialResponse = await HttpClient.GetAsync(urlRegister);
            initialResponse.EnsureSuccessStatusCode();
            var antiForgeryToken = ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>()
            {
                { "Input.Email", UsuarioEmail},
                { "Input.Password", UsuarioSenha},
                { "Input.ConfirmPassword", UsuarioConfirmarSenha},
                { AntiForgeryFieldName, antiForgeryToken}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, urlRegister)
            {
                Content = new FormUrlEncodedContent(formData)
            };

            await HttpClient.SendAsync(postRequest);
        }   

        public void GerarUserSenha()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email().ToLower();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public async Task RealizarLoginApi()
        {
            var userData = new LoginViewModel
            {
                Email = "teste@teste.com",
                Senha = "Teste@123"
            };

            // Recriando o client para evitar configurações de Web
            HttpClient = Factory.CreateClient();

            var response = await HttpClient.PostAsJsonAsync("api/login", userData);
            response.EnsureSuccessStatusCode();
        }

        public async Task RealizarLoginWeb()
        {
            var initialResponse = await HttpClient.GetAsync("/Identity/Account/Login");
            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryToken = ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>
            {
                {AntiForgeryFieldName, antiForgeryToken},
                {"Input.Email", "teste@teste.com"},
                {"Input.Password", "Teste@123"}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Identity/Account/Login")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            await HttpClient.SendAsync(postRequest);
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            Factory.Dispose();
        }
    }
}
