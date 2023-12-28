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
        private readonly IPedidoRepository _repository;

        public PedidoCommandHandler(IPedidoRepository repository, IMediator mediator) : base(mediator)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) 
                return await LancarErrosDeValidacao(message, cancellationToken);
            
            var pedido = await _repository.ObterPedidoRascunhoPorClienteId(message.ClienteId);
            var pedidoItem = new PedidoItem(message.PedidoItemId, message.NomePedidoItem, message.QuantidadePedidoItem, message.ValorUnitarioPedidoItem);

            pedido = pedido is null ? NovoPedido(message, pedidoItem) : 
                                      PedidoExistente(pedido, pedidoItem);

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClienteId,
                                                                 pedido.Id,
                                                                 pedidoItem.Id,
                                                                 pedidoItem.Nome,
                                                                 pedidoItem.ValorUnitario,
                                                                 pedidoItem.Quantidade));

            return await Commit(_repository);
        }

        private Pedido NovoPedido(AdicionarItemPedidoCommand message, PedidoItem pedidoItem)
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId);
            
            pedido.AdicionarItem(pedidoItem);
            
            _repository.Adicionar(pedido);
            
            return pedido;
        }

        private Pedido PedidoExistente(Pedido pedido, PedidoItem pedidoItem)
        {
            var pedidoItemExistente = pedido.ExistePedidoItem(pedidoItem.Id);
            
            pedido.AdicionarItem(pedidoItem);
            
            if (pedidoItemExistente)
                _repository.AtualizarItem(pedidoItem);
            else
                _repository.AdicionarItem(pedidoItem);

            _repository.Atualizar(pedido);
            
            return pedido;
        }
    }
}
