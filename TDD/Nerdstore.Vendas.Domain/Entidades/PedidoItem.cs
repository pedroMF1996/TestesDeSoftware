﻿using Nerdstore.Core.DomainObjects;

namespace Nerdstore.Vendas.Domain.Entidades
{
    public class PedidoItem : Entity
    {
        public Guid ProdutoId { get; set; }
        public Guid PedidoId { get; set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        // EF Rel.
        public Pedido Pedido { get; set; }

        protected PedidoItem() { }
        public PedidoItem(Guid id, string nome, int quantidade, decimal valor)
        {
            
            if (quantidade < Pedido.MIN_UNIDADES_ITEM) throw new DomainException($"Minimo de {Pedido.MIN_UNIDADES_ITEM} unidades por produto");
            Id = id;
            ProdutoId = id;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valor;
        }
        
        public void AdicionaQuantidade(int quantidade)
        {
            Quantidade += quantidade;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }
    }
}
