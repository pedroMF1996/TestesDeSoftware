using NerdStore.BDD.Tests.Config;

namespace NerdStore.BDD.Tests.Pedido
{
    public class PedidoTela : PageObjectModel
    {
        public PedidoTela(SeleniumHelper helper) : base(helper)
        {
        }

        public void AcessarVitrineDeProdutos()
        {
            Helper.IrParaUrl(Helper.Configuration.VitrineUrl);
        }

        public void ObterDetalhesDoProduto(int posicao = 1)
        {
            var caminho = $"/html/body/div/main/div/div/div[{posicao}]/span/a";
            Helper.ClicarPorXPath(caminho);
        }

        public bool ValidarProdutoDisponivel()
        {
            return Helper.ValidarConteudoUrl(Helper.Configuration.ProdutoUrl);
        }

        public int ObterQuantidadeEmEstoque()
        {
            var elemento = Helper.ObterTextoElementoPorId("quantidadeEmEstoque");
            var quantidade = elemento.ApenasNumeros();

            if (char.IsDigit(quantidade.ToString(), 0)) return quantidade;

            return 0;
        }

        public void ClicarEmComprarAgora()
        {
            var caminho = "/html/body/div/main/div/div/div[2]/form/div[2]/button";
            Helper.ClicarPorXPath(caminho);
        }

        public bool ValidarSeEstaNoCarrinhoDeCompra()
        {
            return Helper.ValidarConteudoUrl(Helper.Configuration.CarrinhoUrl);
        }

        public decimal ObterValorUnitarioProdutoCarrinho()
        {
            return Convert.ToDecimal(Helper.ObterTextoElementoPorId("valorUnitario").LimparValor());
        }

        public decimal ObterValorTotalCarrinho()
        {
            return Convert.ToDecimal(Helper.ObterTextoElementoPorId("valorTotalCarrinho").LimparValor());
        }

        public decimal ObterValorQuantidade()
        {
            return Convert.ToDecimal(Helper.ObterValorTextBoxPorId("quantidade").LimparValor());
        }

        public void ClicarEmAdicionarQuantidadeItens(int quantidade)
        {
            var botaoAdicionar = Helper.ObterElementoPorClasse("btn-plus");
            if (botaoAdicionar == null) { return; }

            for (int i = 0; i < quantidade; i++)
            {
                botaoAdicionar.Click();
            }
        }

        internal string ObterMensagemDeErro()
        {
            return Helper.ObterTextoElementoPorClasseCSS("alert-danger");
        }
    }
}
