using NerdStore.BDD.Tests.Config;
using NerdStore.BDD.Tests.Usuario;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.BDD.Tests.Pedido
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoFixureCollection))]
    public class Pedido_AdicionarItemAoCarrinhoStepDefinitions : IClassFixture<AutomacaoWebFixture>
    {
        private readonly AutomacaoWebFixture _fixture;
        private readonly PedidoTela _pedidoTela;
        private readonly LoginDeUsuarioTela _loginDeUsuarioTela;
        private string urlProduto;
        public Pedido_AdicionarItemAoCarrinhoStepDefinitions(AutomacaoWebFixture fixture)
        {
            _fixture = fixture;
            _pedidoTela = new PedidoTela(_fixture.BrowserHelper);
            _loginDeUsuarioTela = new LoginDeUsuarioTela(_fixture.BrowserHelper);
        }

        [Given(@"O usuario esteja logado")]
        public void GivenOUsuarioEstejaLogado()
        {
            //Arrange
            var usuario = new UsuarioModel() { Email = "teste@teste.com", Senha = "Teste@123" };

            //Act & Assert
            Assert.True(_loginDeUsuarioTela.Login(usuario));
        }
        
        [Given(@"Que um produto esteja na vitrine")]
        public void GivenQueUmProdutoEstejaNaVitrine()
        {
            //Arrange
            _pedidoTela.AcessarVitrineDeProdutos();
            
            //Act
            _pedidoTela.ObterDetalhesDoProduto(2);
            urlProduto = _pedidoTela.ObterUrl();

            //Assert
            Assert.True(_pedidoTela.ValidarProdutoDisponivel());
        }

        [Given(@"E esteja disponivel no estoque")]
        public void GivenEEstejaDisponivelNoEstoque()
        {
            //Arrange

            //Act

            //Assert
            Assert.True(_pedidoTela.ObterQuantidadeEmEstoque() > 0);
        }

        [When(@"O usuario adicionar uma unidade ao carrinho")]
        public void WhenOUsuarioAdicionarUmaUnidadeAoCarrinho()
        {
            //Arrange

            //Act
            _pedidoTela.ClicarEmComprarAgora();

            //Assert
        }
         
        [Then(@"O usuario sera redirecionado ao resumo da compra")]
        public void ThenOUsuarioSeraRedirecionadoAoResumoDaCompra()
        {
            //Arrange

            //Act

            //Assert
            Assert.True(_pedidoTela.ValidarSeEstaNoCarrinhoDeCompra());
        }

        [Then(@"O Valor Total Do Pedido Sera Exatamente O Valor do Item Adicionado")]
        public void ThenOValorTotalDoPedidoSeraExatamenteOValorDoItemAdicionado()
        {
            //Arrange
            var valorUnitario = _pedidoTela.ObterValorUnitarioProdutoCarrinho();
            var quantidade = _pedidoTela.ObterValorQuantidade();
            var valorTotal = _pedidoTela.ObterValorTotalCarrinho();

            //Act

            //Assert
            Assert.Equal(valorUnitario*quantidade, valorTotal);
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
