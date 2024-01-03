
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nerdstore.Core.Data;
using Nerdstore.Vendas.Domain.Entidades;

namespace Nerdstore.Vendas.Data
{
    public class VendasContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public VendasContext(IMediator mediator, DbContextOptions opt) : base(opt) 
        {
            _mediator = mediator;
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;

            if (sucesso)
                await _mediator.PublicarEventos(this);

            return sucesso;
        }
    }
}
