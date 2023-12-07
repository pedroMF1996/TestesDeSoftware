using Nerdstore.Core.Data;
using Nerdstore.Vendas.Domain.Entidades;

namespace Nerdstore.Vendas.Domain.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        void Adicionar(Pedido pedido);
    }
}
