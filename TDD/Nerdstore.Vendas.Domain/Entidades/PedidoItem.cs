using Nerdstore.Core.DomainObjects;

namespace Nerdstore.Vendas.Domain.Entidades
{
    public class PedidoItem : Entity
    {
        public Guid ProdutoId { get; set; }
        public Guid PedidoId { get; set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        // EF Rel.
        public Pedido Pedido { get; set; }

        protected PedidoItem() { }
        public PedidoItem(Guid id, string nome, int quantidade, decimal valor)
        {
            
            if (quantidade < Pedido.MIN_UNIDADES_ITEM) throw new DomainException($"Minimo de {Pedido.MIN_UNIDADES_ITEM} unidades por produto");
            ProdutoId = id;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valor;
        }

        public void AssociarPedido(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public void AdicionaQuantidade(int quantidade)
        {
            Quantidade += quantidade;
        }
        
        public void AlterarQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }

        public void RecuperarId(Guid id)
        {
            Id = id;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }
    }
}
