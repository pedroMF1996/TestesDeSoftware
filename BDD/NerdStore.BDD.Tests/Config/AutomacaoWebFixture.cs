using Bogus;
using NerdStore.BDD.Tests.Usuario;
using Xunit;

namespace NerdStore.BDD.Tests.Config
{
    [CollectionDefinition(nameof(AutomacaoFixureCollection))]
    public class AutomacaoFixureCollection : ICollectionFixture<AutomacaoWebFixture>
    { }

    public class AutomacaoWebFixture : IDisposable
    {

        private readonly Faker<UsuarioModel> usuarioFaker;
        public UsuarioModel Usuario;
        public SeleniumHelper BrowserHelper;
        public readonly ConfigurationHelper Configuration;

        public AutomacaoWebFixture()
        {
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Browser.Chrome, Configuration, true);
            usuarioFaker = new Faker<UsuarioModel>();

        }

        public void GerarDadosDoUsuario()
        {
            Usuario = usuarioFaker.CustomInstantiator(f => new UsuarioModel()
            {
                Email = f.Internet.Email(),
                Senha = f.Internet.Password(8, false, "", "@1Ab_")
            });
        }

        public void Dispose()
        {
            BrowserHelper.Dispose();
        }
    }
}
