using Microsoft.AspNetCore.Mvc.Testing;
using NerdStore.WebApp.MVC;

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

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions()
            {
                BaseAddress = new Uri("https://localhost")
            };

            Factory = new LojaAppFacory<TStartup>();
            HttpClient = Factory.CreateClient(clientOptions);
        }



        public void Dispose()
        {
            HttpClient.Dispose();
            Factory.Dispose();
        }
    }
}
