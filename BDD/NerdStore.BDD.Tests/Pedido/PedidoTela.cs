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
            var caminnho = "/html/body/div/main/div/div/div[2]/p[1]";
            var elemento = Helper.ObterElementoPorXPath(caminnho);
            var quantidade = elemento.Text.ApenasNumeros();

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


    }
}
