
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nerdstore.Core.Data;
using Nerdstore.Core.DomainObjects;
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class VendasContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

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

    public static class MediatrExtensions
    {
        public static async Task PublicarEventos<T>(this IMediator mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearNotifications());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
