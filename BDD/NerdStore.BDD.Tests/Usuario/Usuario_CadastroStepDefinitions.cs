using System;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    public class Usuario_CadastroStepDefinitions
    {
        [When(@"Ele clicar em registrar")]
        public void WhenEleClicarEmRegistrar()
        {
            throw new PendingStepException();
        }

        [When(@"Preencher os dados do formulario")]
        public void WhenPreencherOsDadosDoFormulario(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"Clicar no botao registrar")]
        public void WhenClicarNoBotaoRegistrar()
        {
            throw new PendingStepException();
        }

        [When(@"Preencher os dados do formulario com uma senha sem maiusculas")]
        public void WhenPreencherOsDadosDoFormularioComUmaSenhaSemMaiusculas(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"Preencher os dados do formulario com uma senha sem caracteres especiais")]
        public void WhenPreencherOsDadosDoFormularioComUmaSenhaSemCaracteresEspeciais(Table table)
        {
            throw new PendingStepException();
        }
    }
}
