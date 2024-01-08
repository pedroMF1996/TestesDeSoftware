using System;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    public class Usuario_LoginStepDefinitions
    {
        [When(@"Ele clicar em login")]
        public void WhenEleClicarEmLogin()
        {
            throw new PendingStepException();
        }

        [When(@"Clicar no botao Login")]
        public void WhenClicarNoBotaoLogin()
        {
            throw new PendingStepException();
        }

        [When(@"Preencher os dados do formulario de login")]
        public void WhenPreencherOsDadosDoFormularioDeLogin(Table table)
        {
            throw new PendingStepException();
        }
    }
}
