using System.Collections.ObjectModel;

namespace Nerdstore.Vendas.Domain.Entidades
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public decimal ValorTotal { get; private set; }
        private Collection<PedidoItem> _pedidoItems = new Collection<PedidoItem>();
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public Pedido()
        {
            Id = Guid.NewGuid();
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            _pedidoItems.Add(pedidoItem);
            ValorTotal = PedidoItems.Sum(i => i.Quantidade * i.ValorUnitario);
        }
    }
}
