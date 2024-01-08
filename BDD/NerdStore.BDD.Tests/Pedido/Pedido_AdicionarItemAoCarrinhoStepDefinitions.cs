using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Pedido
{
    [Binding]
    public class Pedido_AdicionarItemAoCarrinhoStepDefinitions
    {
        [Given(@"Que um produto esteja na vitrine")]
        public void GivenQueUmProdutoEstejaNaVitrine()
        {
            //Arrange

            //Act

            //Assert
        }

        [Given(@"E esteja disponivel no estoque")]
        public void GivenEEstejaDisponivelNoEstoque()
        {
            //Arrange

            //Act

            //Assert
        }

        [Given(@"O usuario esteja logado")]
        public void GivenOUsuarioEstejaLogado()
        {
            //Arrange

            //Act

            //Assert
        }

        [When(@"O usuario adicionar uma unidade ao carrinho")]
        public void WhenOUsuarioAdicionarUmaUnidadeAoCarrinho()
        {
            //Arrange

            //Act

            //Assert
        }

        [Then(@"O usuario sera redirecionado ao resumo da compra")]
        public void ThenOUsuarioSeraRedirecionadoAoResumoDaCompra()
        {
            //Arrange

            //Act

            //Assert
        }

        [Then(@"O Valor Total Do Pedido Sera Exatamente O Valor do Item Adicionado")]
        public void ThenOValorTotalDoPedidoSeraExatamenteOValorDoItemAdicionado()
        {
            //Arrange

            //Act

            //Assert
        }

        [When(@"O usuario adicionar um item acima da quantidade maxima permitida")]
        public void WhenOUsuarioAdicionarUmItemAcimaDaQuantidadeMaximaPermitida()
        {
            //Arrange

            //Act

            //Assert
        }

        [Then(@"Recebera uma mensagem de erro mencionando que foi ultrapassado a quantidade limite")]
        public void ThenReceberaUmaMensagemDeErroMencionandoQueFoiUltrapassadoAQuantidadeLimite()
        {
            //Arrange

            //Act

            //Assert
        }

        [Given(@"O mesmo produto ja tenha sido adicionado no carrinho anteriormente")]
        public void GivenOMesmoProdutoJaTenhaSidoAdicionadoNoCarrinhoAnteriormente()
        {
            //Arrange

            //Act

            //Assert
        }

        [Then(@"A quantidade de items daquele produto tera sido acrescida em uma unidade a mais")]
        public void ThenAQuantidadeDeItemsDaqueleProdutoTeraSidoAcrescidaEmUmaUnidadeAMais()
        {
            //Arrange

            //Act

            //Assert
        }

        [Then(@"O valor total do pedido sera a multiplicacao da quantidade de itens pelo valor unitario")]
        public void ThenOValorTotalDoPedidoSeraAMultiplicacaoDaQuantidadeDeItensPeloValorUnitario()
        {
            //Arrange

            //Act

            //Assert
        }

        [When(@"O usuario adicionar a quantidade maxima permitida ao carrinho")]
        public void WhenOUsuarioAdicionarAQuantidadeMaximaPermitidaAoCarrinho()
        {
            //Arrange

            //Act

            //Assert
        }

        [Then(@"Recebera uma mensagem de erro mencionando que foi ultrapassada a quantidade limite maximo")]
        public void ThenReceberaUmaMensagemDeErroMencionandoQueFoiUltrapassadaAQuantidadeLimiteMaximo()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
