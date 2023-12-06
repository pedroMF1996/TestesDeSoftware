namespace Nerdstore.Vendas.Domain.Entidades
{
    public class PedidoItem
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public PedidoItem(Guid id, string nome, int quantidade, decimal valor)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valor;
        }

        public void AdicionaQuantidade(int quantidade)
        {
            Quantidade += quantidade;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }
    }
}
