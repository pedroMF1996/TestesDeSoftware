using System.Collections.ObjectModel;

namespace Nerdstore.Vendas.Domain.Entidades
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public decimal ValorTotal { get; private set; }
        public StatusPedido StatusPedido { get; private set; }
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
        private Collection<PedidoItem> _pedidoItems = new Collection<PedidoItem>();

        protected Pedido()
        {
            Id = Guid.NewGuid();
            TornarRascunho();
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            pedidoItem = TratarPedidoItemExistente(pedidoItem);

            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());
        }

        public void TornarRascunho()
        {
            StatusPedido = StatusPedido.Rascunho;
        }

        private PedidoItem TratarPedidoItemExistente(PedidoItem pedidoItem)
        {
            var pedidoItemExistente = _pedidoItems.FirstOrDefault(i => i.Id == pedidoItem.Id);
            if (pedidoItemExistente is not null)
            {
                _pedidoItems.Remove(pedidoItemExistente);
                pedidoItemExistente.AdicionaQuantidade(pedidoItem.Quantidade);
                return pedidoItemExistente;
            }
            return pedidoItem;
        }
        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho()
            {
                return new Pedido();
            }
        }
    }

    public enum StatusPedido
    {
        Rascunho,
        Iniciado,
        Pago,
        Entregue,
        Cancelado
    }

    public class Cliente
    {
        public Guid Id { get; private set; }

        public Cliente()
        {
            Id = Guid.NewGuid();
        }
    }
}
