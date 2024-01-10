using NerdStore.BDD.Tests.Config;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoFixureCollection))]
    public class CommonSteps : IClassFixture<AutomacaoWebFixture>
    {
        private readonly AutomacaoWebFixture _fixture;
        private readonly CadastroDeUsuarioTela _cadastroDeUsuarioTela;

        public CommonSteps(AutomacaoWebFixture fixture)
        {
            _fixture = fixture;
            _cadastroDeUsuarioTela = new CadastroDeUsuarioTela(_fixture.BrowserHelper);
        }

        [Given(@"Que um visitante esta acessando o site da loja")]
        public void GivenQueUmVisitanteEstaAcessandoOSiteDaLoja()
        {
            //Arrange

            //Act
            _cadastroDeUsuarioTela.AcessarSiteLoja();

            //Assert
            Assert.Equal(_fixture.Configuration.VitrineUrl, _cadastroDeUsuarioTela.ObterUrl());
        }

        [Then(@"Uma saudacao com seu e-mail sera exibida no menu superior")]
        public void ThenUmaSaudacaoComSeuE_MailSeraExibidaNoMenuSuperior()
        {
            //Arrange

            //Act

            //Assert
            Assert.Equal(_fixture.Configuration.VitrineUrl, _cadastroDeUsuarioTela.ObterUrl());
        }

        [Then(@"Ele sera redirecionado para a vitrine")]
        public void ThenEleSeraRedirecionadoParaAVitrine()
        {
            //Arrange

            //Act

            //Assert
            Assert.True(_cadastroDeUsuarioTela.ValidarSaudacaoUsuario(_fixture.Usuario));
        }
    }
}
