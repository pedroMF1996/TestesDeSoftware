using FluentValidation.Results;
using Nerdstore.Core.DomainObjects;
using System.Collections.ObjectModel;

namespace Nerdstore.Vendas.Domain.Entidades
{
    public class Pedido : Entity, IAggregateRoot
    {
        public static int MAX_UNIDADES_ITEM = 15;
        public static int MIN_UNIDADES_ITEM = 1;

        public int Codigo { get; private set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        public decimal Desconto { get; private set; }
        public Guid? VoucherId { get; private set; }
        public Voucher Voucher { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
        private Collection<PedidoItem> _pedidoItems = new Collection<PedidoItem>();

        protected Pedido()
        {
            DataCadastro = DateTime.Now;
            TornarRascunho();
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            TratarNumeroMaximoItemPedido(pedidoItem);

            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void AtualizarUnidades(PedidoItem pedidoItem, int quantidade)
        {
            TratarNumeroMaximoItemPedido(pedidoItem);

            AtualizarQuantidadePedidoItemExistente(ref pedidoItem, quantidade);

            TratarNumeroMaximoItemPedido(pedidoItem);
            
            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            TratarPedidoItemInexistente(pedidoItem);
            TratarNumeroMaximoItemPedido(pedidoItem);

            var pedidoItemExistente = ExistePedidoItem(pedidoItem.Id);

            _pedidoItems.Remove(pedidoItemExistente);            
            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            TratarPedidoItemInexistente(pedidoItem);

            _pedidoItems.Remove(pedidoItem);

            CalcularValorPedido();
        }

        public PedidoItem ExistePedidoItem(Guid pedidoItemId)
        {
            return PedidoItems.FirstOrDefault(i => i.Id == pedidoItemId);
        }

        private void AtualizarQuantidadePedidoItemExistente(ref PedidoItem pedidoItem, int quantidade)
        {
            if (ExistePedidoItem(pedidoItem.Id) is PedidoItem pedidoItemExistente)
            {
                _pedidoItems.Remove(pedidoItemExistente);
                pedidoItemExistente.AdicionaQuantidade(quantidade);
                pedidoItem = pedidoItemExistente;
            }
        }

        private void TratarNumeroMaximoItemPedido(PedidoItem pedidoItem)
        {
            if (ExistePedidoItem(pedidoItem.Id) is PedidoItem pedidoExistente 
                    && ( pedidoExistente.Quantidade > MAX_UNIDADES_ITEM || pedidoExistente.Quantidade + pedidoItem.Quantidade > MAX_UNIDADES_ITEM)
                    || pedidoItem.Quantidade > MAX_UNIDADES_ITEM)
                throw new DomainException($"Maximo de {MAX_UNIDADES_ITEM} unidades por produto");
        }

        private void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());

            CalcularValorTotalDesconto();
        }

        private void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        private void TratarPedidoItemInexistente(PedidoItem pedidoItem)
        {
            if (!_pedidoItems.Any(i => i.Id == pedidoItem.Id)) throw new DomainException("Produto inexistente");
        }

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var result = voucher.ValidarSeAplicavel();
            if (!result.IsValid) return result;

            voucher.AplicarVoucher();
            Voucher = voucher;
            VoucherUtilizado = true;

            CalcularValorTotalDesconto();

            return result;
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;
            if (TratarDescontoMaiorQueValorTotal()) return;
            
            var valorTotalOrigem = ValorTotal;

            if(Voucher.TipoDesconto == TipoDesconto.Valor && Voucher.ValorDesconto.HasValue)
                ValorTotal -= Voucher.ValorDesconto.Value;
            else if(Voucher.PercentualDesconto.HasValue)
                ValorTotal *= (1 - Voucher.PercentualDesconto.Value / 100);

            Desconto = valorTotalOrigem - ValorTotal;
        }

        private bool TratarDescontoMaiorQueValorTotal()
        {
            if (VerificaValorDescontoMaiorQueValorTotal() 
                || VerificaValorPorcentagemMaiorQueValorTotal())
            {
                ValorTotal = 0;
                return true;
            }
            return false;
        }

        private bool VerificaValorPorcentagemMaiorQueValorTotal()
        {
            return Voucher.PercentualDesconto.HasValue && Voucher.PercentualDesconto.Value > 100;
        }

        private bool VerificaValorDescontoMaiorQueValorTotal()
        {
            return Voucher.ValorDesconto.HasValue && Voucher.ValorDesconto.Value > ValorTotal;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido() 
                {
                    ClienteId = clienteId,
                };
                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}
