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

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {

        public readonly LojaAppFacory<TStartup> Factory;
        public HttpClient HttpClient;
        public string AntiForgeryFieldName = "__RequestVerificationToken";
        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions()
            {
                BaseAddress = new Uri("https://localhost")
            };

            Factory = new LojaAppFacory<TStartup>();
            HttpClient = Factory.CreateClient(clientOptions);
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
