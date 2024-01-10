using NerdStore.BDD.Tests.Config;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoFixureCollection))]
    public class Usuario_LoginStepDefinitions : IClassFixture<AutomacaoWebFixture>
    {

        private readonly AutomacaoWebFixture _fixture;
        private readonly LoginDeUsuarioTela _loginDeUsuarioTela;
        public Usuario_LoginStepDefinitions(AutomacaoWebFixture fixture)
        {
            _fixture = fixture;
            _loginDeUsuarioTela = new LoginDeUsuarioTela(_fixture.BrowserHelper);
        }

        [When(@"Ele clicar em login")]
        public void WhenEleClicarEmLogin()
        {
            //Arrange

            //Act
            _loginDeUsuarioTela.ClicarNoLogin();

            //Assert
            Assert.Equal(_fixture.Configuration.LoginUrl, _loginDeUsuarioTela.ObterUrl());
        }

        [When(@"Clicar no botao Login")]
        public void WhenClicarNoBotaoLogin()
        {
            //Arrange

            //Act
            _loginDeUsuarioTela.ClicarNoBotaoDeLogin();

            //Assert
        }

        [When(@"Preencher os dados do formulario de login")]
        public void WhenPreencherOsDadosDoFormularioDeLogin(Table table)
        {
            //Arrange
            var usuario = new UsuarioModel() { Email = "teste@teste.com", Senha = "Teste@123" };
            _fixture.Usuario = usuario;

            //Act
            _loginDeUsuarioTela.PreencherUsuarioESenha(usuario);

            //Assert
            _loginDeUsuarioTela.ValidarPreenchimentoFormularioLogin(usuario);
        }
    }
}
