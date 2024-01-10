using Bogus;
using NerdStore.BDD.Tests.Config;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoFixureCollection))]
    public class Usuario_CadastroStepDefinitions : IClassFixture<AutomacaoWebFixture>
    {
        private readonly AutomacaoWebFixture _fixture;
        private readonly CadastroDeUsuarioTela _cadastroDeUsuarioTela;
        
        public Usuario_CadastroStepDefinitions(AutomacaoWebFixture fixture)
        {
            _fixture = fixture;
            _cadastroDeUsuarioTela = new CadastroDeUsuarioTela(_fixture.BrowserHelper);    
        }

        [When(@"Ele clicar em registrar")]
        public void WhenEleClicarEmRegistrar()
        {
            //Arrange

            //Act
            _cadastroDeUsuarioTela.ClicarNoLinkRegistrar();

            //Assert
            Assert.Equal(_fixture.Configuration.RegisterUrl, _cadastroDeUsuarioTela.ObterUrl());
        }

        [When(@"Preencher os dados do formulario")]
        public void WhenPreencherOsDadosDoFormulario(Table table)
        {
            //Arrange
            _fixture.GerarDadosDoUsuario();
            var usuario = _fixture.Usuario;

            //Act
            _cadastroDeUsuarioTela.PreencherFormularioRegistro(usuario);

            //Assert
            Assert.True(_cadastroDeUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }

        [When(@"Clicar no botao registrar")]
        public void WhenClicarNoBotaoRegistrar()
        {
            //Arrange

            //Act
            _cadastroDeUsuarioTela.ClicarNoBotaoRegistrar();

            //Assert

        }

        [When(@"Preencher os dados do formulario com uma senha sem maiusculas")]
        public void WhenPreencherOsDadosDoFormularioComUmaSenhaSemMaiusculas(Table table)
        {
            //Arrange
            _fixture.GerarDadosDoUsuario();
            var usuario = _fixture.Usuario;
            usuario.Senha = "teste@123";

            //Act
            _cadastroDeUsuarioTela.PreencherFormularioRegistro(usuario);

            //Assert
            Assert.True(_cadastroDeUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }

        [When(@"Preencher os dados do formulario com uma senha sem caracteres especiais")]
        public void WhenPreencherOsDadosDoFormularioComUmaSenhaSemCaracteresEspeciais(Table table)
        {
            //Arrange
            _fixture.GerarDadosDoUsuario();
            var usuario = _fixture.Usuario;
            usuario.Senha = "Teste123";

            //Act
            _cadastroDeUsuarioTela.PreencherFormularioRegistro(usuario);

            //Assert
            Assert.True(_cadastroDeUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }

        [Then(@"Ele recebera uma mensagem de erro que a senha precisa conter pelo menos uma letra maiuscula")]
        public void ThenEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterPeloMenosUmaLetraMaiuscula()
        {
            //Arrange

            //Act

            //Assert
            
            Assert.True(_cadastroDeUsuarioTela.ValidarMensagemDeErro("Passwords must have at least one uppercase ('A'-'Z')."));
        }

        [Then(@"Ele recebera uma mensagem de erro que a senha precisa conter pelo menos um caracter especial")]
        public void ThenEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterPeloMenosUmCaracterEspecial()
        {
            //Arrange

            //Act

            //Assert
            Assert.True(_cadastroDeUsuarioTela.ValidarMensagemDeErro("Passwords must have at least one non alphanumeric character."));
        }
    }
}
