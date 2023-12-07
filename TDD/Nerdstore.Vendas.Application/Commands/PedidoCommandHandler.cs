using FluentValidation.Results;
using MediatR;
using Nerdstore.Core.Messages;
using Nerdstore.Vendas.Application.Events;
using Nerdstore.Vendas.Domain.Entidades;
using Nerdstore.Vendas.Domain.Repositories;

namespace Nerdstore.Vendas.Application.Commands
{
    public class PedidoCommandHandler : CommandHandler, 
                                        IRequestHandler<AdicionarItemPedidoCommand,ValidationResult>
    {
        private readonly IMediator _mediatr;
        private readonly IPedidoRepository _repository;

        public PedidoCommandHandler(IMediator mediatr, IPedidoRepository repository)
        {
            _mediatr = mediatr;
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;
            
            var pedidoItem = new PedidoItem(message.PedidoItemId, message.NomePedidoItem, message.QuantidadePedidoItem, message.ValorUnitarioPedidoItem);
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId);
            
            pedido.AdicionarItem(pedidoItem);

            _repository.Adicionar(pedido);

            pedido.AddNotification(new PedidoItemAdicionadoEvent(pedido.ClienteId,
                                                                 pedido.Id,
                                                                 pedidoItem.Id,
                                                                 pedidoItem.Nome,
                                                                 pedidoItem.ValorUnitario,
                                                                 pedidoItem.Quantidade));

            return await Commit(_repository);
        }
    }
}
