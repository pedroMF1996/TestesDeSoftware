using Microsoft.VisualStudio.TestPlatform.TestHost;
using NerStore.Integrations.Tests;

namespace NerdStore.WebApp.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class PedidoApiTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;

        public PedidoApiTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        
    }
}