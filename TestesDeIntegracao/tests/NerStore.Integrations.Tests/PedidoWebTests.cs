using Microsoft.VisualStudio.TestPlatform.TestHost;
using NerStore.Integrations.Tests.Config;

namespace NerdStore.WebApp.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class PedidoWebTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;

        public PedidoWebTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        
    }
}