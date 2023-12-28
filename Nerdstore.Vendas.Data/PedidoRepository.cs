using Nerdstore.Core.Data;
using Nerdstore.Vendas.Domain.Entidades;
using Nerdstore.Vendas.Domain.Repositories;

namespace Nerdstore.Vendas.Data
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasContext _vendasContext;

        public PedidoRepository(VendasContext vendasContext)
        {
            _vendasContext = vendasContext;
        }

        public IUnitOfWork UnitOfWork => _vendasContext;

        public void Adicionar(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _vendasContext?.Dispose();
        }

        public Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
        {
            throw new NotImplementedException();
        }
    }
}
