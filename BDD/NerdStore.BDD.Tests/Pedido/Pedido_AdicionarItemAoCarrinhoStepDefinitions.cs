using NerdStore.BDD.Tests.Config;
using NerdStore.BDD.Tests.Usuario;
using TechTalk.SpecFlow;
using Xunit;
using PedidoEntidade = Nerdstore.Vendas.Domain.Entidades.Pedido;

namespace NerdStore.BDD.Tests.Pedido
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoFixureCollection))]
    public class Pedido_AdicionarItemAoCarrinhoStepDefinitions : IClassFixture<AutomacaoWebFixture>
    {
        private readonly AutomacaoWebFixture _fixture;
        private readonly PedidoTela _pedidoTela;
        private readonly LoginDeUsuarioTela _loginDeUsuarioTela;
        private string _urlProduto;
        private int _quantidadeInicial;
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
            _urlProduto = _pedidoTela.ObterUrl();

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
            var valorTotal = _pedidoTela.ObterValorTotalCarrinho();

            //Act

            //Assert
            Assert.Equal(valorUnitario, valorTotal);
        }

        [When(@"O usuario adicionar um item acima da quantidade maxima permitida")]
        public void WhenOUsuarioAdicionarUmItemAcimaDaQuantidadeMaximaPermitida()
        {
            //Arrange
            _pedidoTela.ClicarEmAdicionarQuantidadeItens(PedidoEntidade.MAX_UNIDADES_ITEM + 1);

            //Act

            //Assert
            _pedidoTela.ClicarEmComprarAgora();
        }

        [Then(@"Recebera uma mensagem de erro mencionando que foi ultrapassado a quantidade limite")]
        public void ThenReceberaUmaMensagemDeErroMencionandoQueFoiUltrapassadoAQuantidadeLimite()
        {
            //Arrange

            //Act

            //Assert
            Assert.Contains($"A quantidade máxima de um item é {PedidoEntidade.MAX_UNIDADES_ITEM}", _pedidoTela.ObterMensagemDeErro());
        }

        [Given(@"O mesmo produto ja tenha sido adicionado no carrinho anteriormente")]
        public void GivenOMesmoProdutoJaTenhaSidoAdicionadoNoCarrinhoAnteriormente()
        {
            //Arrange
            _pedidoTela.NavegarParaCarrinhoDeCompras();
            _pedidoTela.GarantirQueOPrimeiroItemEstejaAdicionado();
            var produtoId = _pedidoTela.ObterIdPrimeiroProdutoNoCarrinho();
            _quantidadeInicial = _pedidoTela.ObterValorQuantidade();

            //Act

            //Assert
            Assert.Contains(_urlProduto, produtoId);
            Assert.True(_pedidoTela.ValidarSeEstaNoCarrinhoDeCompra());
            Assert.True(_quantidadeInicial > 0);

            _pedidoTela.VoltarNavegacao();
        }

        [Then(@"A quantidade de items daquele produto tera sido acrescida em uma unidade a mais")]
        public void ThenAQuantidadeDeItemsDaqueleProdutoTeraSidoAcrescidaEmUmaUnidadeAMais()
        {
            //Arrange

            //Act

            //Assert
            Assert.Equal(_pedidoTela.ObterValorQuantidade(), _quantidadeInicial + 1);
        }

        [Then(@"O valor total do pedido sera a multiplicacao da quantidade de itens pelo valor unitario")]
        public void ThenOValorTotalDoPedidoSeraAMultiplicacaoDaQuantidadeDeItensPeloValorUnitario()
        {
            //Arrange
            var valorUnitario = _pedidoTela.ObterValorUnitarioProdutoCarrinho();
            var valorTotal = _pedidoTela.ObterValorTotalCarrinho();
            var quantidade = _pedidoTela.ObterValorQuantidade();

            //Act

            //Assert
            Assert.Equal(valorUnitario * quantidade, valorTotal);
        }

        [When(@"O usuario adicionar a quantidade maxima permitida ao carrinho")]
        public void WhenOUsuarioAdicionarAQuantidadeMaximaPermitidaAoCarrinho()
        {
            //Arrange
            _pedidoTela.ClicarEmAdicionarQuantidadeItens(PedidoEntidade.MAX_UNIDADES_ITEM);

            //Act
            _pedidoTela.ClicarEmComprarAgora();

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
