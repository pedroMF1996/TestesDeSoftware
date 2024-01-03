using Nerdstore.Core.Messages;

namespace Nerdstore.Vendas.Application.Events
{
    public class PedidoItemAdicionadoEvent : Event
    {
        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid PedidoItemId { get; private set; }
        public string ProdutoNome { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public int Quantidade { get; private set; }

        public PedidoItemAdicionadoEvent(Guid clienteId, Guid pedidoId, Guid pedidoItemId, string produtoNome, decimal valorUnitario, int quantidade)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
            PedidoItemId = pedidoItemId;
            ProdutoNome = produtoNome;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
        }
    }
}
