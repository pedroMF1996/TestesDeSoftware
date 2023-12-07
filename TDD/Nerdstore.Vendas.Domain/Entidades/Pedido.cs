﻿using Nerdstore.Core.DomainObjects;
using System.Collections.ObjectModel;

namespace Nerdstore.Vendas.Domain.Entidades
{
    public class Pedido
    {
        public static int MAX_UNIDADES_ITEM = 15;
        public static int MIN_UNIDADES_ITEM = 1;

        public Guid Id { get; private set; }
        public decimal ValorTotal { get; private set; }
        public StatusPedido StatusPedido { get; private set; }
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
        private Collection<PedidoItem> _pedidoItems = new Collection<PedidoItem>();

        protected Pedido()
        {
            Id = Guid.NewGuid();
            TornarRascunho();
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            TratarNumeroMaximoItemPedido(pedidoItem);

            AtualizarQuantidadePedidoItemExistente(ref pedidoItem, pedidoItem.Id);

            TratarNumeroMaximoItemPedido(pedidoItem);

            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            TratarPedidoItemInexistente(pedidoItem);
            TratarNumeroMaximoItemPedido(pedidoItem);

            var pedidoItemExistente = _pedidoItems.FirstOrDefault(i => i.Id == pedidoItem.Id);

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

        private void AtualizarQuantidadePedidoItemExistente(ref PedidoItem pedidoItem, Guid pedidoId)
        {
            var pedidoItemExistente = _pedidoItems.FirstOrDefault(i => i.Id == pedidoId);
            if (pedidoItemExistente is not null)
            {
                _pedidoItems.Remove(pedidoItemExistente);
                pedidoItemExistente.AdicionaQuantidade(pedidoItem.Quantidade);
                pedidoItem = pedidoItemExistente;
            }
        }

        private void TratarNumeroMaximoItemPedido(PedidoItem pedidoItem)
        {
            if (pedidoItem.Quantidade > MAX_UNIDADES_ITEM) throw new DomainException($"Maximo de {MAX_UNIDADES_ITEM} unidades por produto");
        }

        private void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());
        }

        private void TornarRascunho()
        {
            StatusPedido = StatusPedido.Rascunho;
        }

        private void TratarPedidoItemInexistente(PedidoItem pedidoItem)
        {
            if (!_pedidoItems.Any(i => i.Id == pedidoItem.Id)) throw new DomainException("Produto inexistente");
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho()
            {
                return new Pedido();
            }
        }
    }

    public enum StatusPedido
    {
        Rascunho,
        Iniciado,
        Pago,
        Entregue,
        Cancelado
    }

    public class Cliente
    {
        public Guid Id { get; private set; }

        public Cliente()
        {
            Id = Guid.NewGuid();
        }
    }
}
