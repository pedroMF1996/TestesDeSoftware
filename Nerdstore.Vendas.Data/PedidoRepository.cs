using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId)
        {
            return await _vendasContext.Pedidos.AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();
        }

        public async Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
        {
            var pedido = await _vendasContext.Pedidos.FirstOrDefaultAsync(p => p.ClienteId == clienteId && p.PedidoStatus == PedidoStatus.Rascunho);
            if (pedido == null) return null;

            await _vendasContext.Entry(pedido)
                .Collection(i => i.PedidoItems).LoadAsync();

            if (pedido.VoucherId != null)
            {
                await _vendasContext.Entry(pedido)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            return pedido;
        }

        public void Adicionar(Pedido pedido)
        {
            _vendasContext.Pedidos.Add(pedido);
        }

        public void Atualizar(Pedido pedido)
        {
            _vendasContext.Pedidos.Update(pedido);
        }


        public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
        {
            return await _vendasContext.PedidoItems.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            _vendasContext.PedidoItems.Add(pedidoItem);
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            _vendasContext.PedidoItems.Update(pedidoItem);
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            _vendasContext.PedidoItems.Remove(pedidoItem);
        }

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await _vendasContext.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public void Dispose()
        {
            _vendasContext.Dispose();
        }
    }
}
