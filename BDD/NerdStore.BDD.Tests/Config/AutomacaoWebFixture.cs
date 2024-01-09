using Xunit;

namespace NerdStore.BDD.Tests.Config
{
    [CollectionDefinition(nameof(AutomacaoFixureCollection))]
    public class AutomacaoFixureCollection : ICollectionFixture<AutomacaoWebFixture>
    { }

    public class AutomacaoWebFixture
    {
        public SeleniumHelper BrowserHelper;
        public readonly ConfigurationHelper Configuration;

        public AutomacaoWebFixture()
        {
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Browser.Chrome, Configuration);
        }

    }
}
