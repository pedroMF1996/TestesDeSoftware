using Nerdstore.Core.Data;
using Nerdstore.Vendas.Domain.Entidades;

namespace Nerdstore.Vendas.Domain.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        void Adicionar(Pedido pedido);
        void AdicionarItem(PedidoItem pedidoItem);
        void Atualizar(Pedido pedido);
        void AtualizarItem(PedidoItem pedidoItem);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);
    }
}
