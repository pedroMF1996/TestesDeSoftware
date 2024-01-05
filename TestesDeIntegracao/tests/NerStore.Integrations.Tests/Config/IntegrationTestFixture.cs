using Bogus;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.Testing;
using NerdStore.WebApp.MVC;
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
        public readonly string UsuarioEmail;
        public readonly string UsuarioSenha;
        public readonly string UsuarioConfirmarSenha;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions()
            {
                BaseAddress = new Uri("https://localhost")
            };

            Factory = new LojaAppFacory<TStartup>();
            HttpClient = Factory.CreateClient(clientOptions);
            var faker = new Faker("pt_BR");

            #region Credenciais

            UsuarioEmail = faker.Internet.Email();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
            UsuarioConfirmarSenha = UsuarioSenha;

            #endregion

        }

        public string ObiterAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch =
                Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;

            throw new ArgumentException($"AntiForgeryToken {AntiForgeryFieldName} nao encontrado no HTML {htmlBody}");
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            Factory.Dispose();
        }
    }
}
