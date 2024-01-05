using FluentValidation.Results;
using MediatR;
using Nerdstore.Core.Messages;
using Nerdstore.Vendas.Application.Events;
using Nerdstore.Vendas.Domain.Entidades;
using Nerdstore.Vendas.Domain.Repositories;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Events;

namespace Nerdstore.Vendas.Application.Commands
{
    public class PedidoCommandHandler : CommandHandler, 
                                        IRequestHandler<AdicionarItemPedidoCommand,ValidationResult>,
                                        IRequestHandler<AtualizarItemPedidoCommand, ValidationResult>,
                                        IRequestHandler<RemoverItemPedidoCommand, ValidationResult>,
                                        IRequestHandler<AplicarVoucherPedidoCommand, ValidationResult>
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
            
            var pedidoItem = new PedidoItem(message.PedidoItemId, message.NomePedidoItem, message.QuantidadePedidoItem, message.ValorUnitarioPedidoItem);

            var pedido = await _repository.ObterPedidoRascunhoPorClienteId(message.ClienteId);
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

        public async Task<ValidationResult> Handle(AtualizarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return await LancarErrosDeValidacao(message, cancellationToken);

            var pedido = await _repository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if (pedido == null)
            {
                AddErro("Pedido não encontrado!");
                return await LancarErros("pedido", cancellationToken);
            }

            var pedidoItem = await _repository.ObterItemPorPedido(pedido.Id, message.ProdutoId);

            if (pedido.ExistePedidoItem(pedidoItem.ProdutoId) is null)
            {
                AddErro("Item do pedido não encontrado!");
                return await LancarErros("pedido", cancellationToken);
            }

            pedido.AtualizarUnidades(pedidoItem, message.Quantidade);
            pedido.AdicionarEvento(new PedidoProdutoAtualizadoEvent(message.ClienteId, pedido.Id, message.ProdutoId, message.Quantidade));

            _repository.AtualizarItem(pedidoItem);
            _repository.Atualizar(pedido);

            return await Commit(_repository);
        }

        public async Task<ValidationResult> Handle(RemoverItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return await LancarErrosDeValidacao(message, cancellationToken);

            var pedido = await _repository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if (pedido == null)
            {
                AddErro("Pedido não encontrado!");
                return await LancarErros("pedido", cancellationToken);
            }

            var pedidoItem = await _repository.ObterItemPorPedido(pedido.Id, message.ProdutoId);

            if (pedidoItem != null && pedido.ExistePedidoItem(pedidoItem.ProdutoId) is null)
            {
                AddErro("Item do pedido não encontrado!");
                return await LancarErros("pedido", cancellationToken);
            }

            pedido.RemoverItem(pedidoItem);
            pedido.AdicionarEvento(new PedidoProdutoRemovidoEvent(message.ClienteId, pedido.Id, message.ProdutoId));

            _repository.RemoverItem(pedidoItem);
            _repository.Atualizar(pedido);

            return await Commit(_repository);
        }

        public async Task<ValidationResult> Handle(AplicarVoucherPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return await LancarErrosDeValidacao(message, cancellationToken);

            var pedido = await _repository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if (pedido == null)
            {
                AddErro("Pedido não encontrado!");
                return await LancarErros("pedido", cancellationToken);
            }

            var voucher = await _repository.ObterVoucherPorCodigo(message.CodigoVoucher);

            if (voucher == null)
            {
                AddErro("Voucher não encontrado!");
                return await LancarErros("pedido", cancellationToken);
            }

            var voucherAplicacaoValidation = pedido.AplicarVoucher(voucher);
            
            if (!voucherAplicacaoValidation.IsValid)
                return await LancarErrosDeValidacao("pedido", voucherAplicacaoValidation, cancellationToken);
            

            pedido.AdicionarEvento(new VoucherAplicadoPedidoEvent(message.ClienteId, pedido.Id, voucher.Id));

            _repository.Atualizar(pedido);

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
            if (pedido.ExistePedidoItem(pedidoItem.ProdutoId) is PedidoItem pedidoItemExistente)
            {
                pedidoItemExistente.AlterarQuantidade(pedidoItem.Quantidade);
                _repository.AtualizarItem(pedidoItemExistente);
            }
            else
            {
                pedido.AdicionarItem(pedidoItem);
                _repository.AdicionarItem(pedidoItem);
            }

            _repository.Atualizar(pedido);
            
            return pedido;
        }
    }
}
