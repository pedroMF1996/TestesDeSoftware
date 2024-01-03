using FluentValidation;
using Nerdstore.Core.Messages;
using Nerdstore.Vendas.Domain.Entidades;

namespace Nerdstore.Vendas.Application.Commands
{
    public class AdicionarItemPedidoCommand : Command
    {
        public Guid ClienteId { get; private set; }
        public Guid PedidoItemId { get; private set; }
        public string NomePedidoItem { get; private set; }
        public int QuantidadePedidoItem { get; private set; }
        public decimal ValorUnitarioPedidoItem { get; private set; }

        public AdicionarItemPedidoCommand(Guid guid1, Guid guid2, string v1, int v2, decimal v3)
        {
            ClienteId = guid1;
            PedidoItemId = guid2;
            NomePedidoItem = v1;
            QuantidadePedidoItem = v2;
            ValorUnitarioPedidoItem = v3;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarItemPedidoCommandValidation().Validate(this);
            return base.EhValido();
        }
    }

    public class AdicionarItemPedidoCommandValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public static string IdClienteErroMsg => "Id do cliente inválido";
        public static string IdProdutoErroMsg => "Id do produto inválido";
        public static string NomeErroMsg => "O nome do produto não foi informado";
        public static string QtdMaxErroMsg => $"A quantidade máxima de um item é {Pedido.MAX_UNIDADES_ITEM}";
        public static string QtdMinErroMsg => "A quantidade miníma de um item é 1";
        public static string ValorErroMsg => "O valor do item precisa ser maior que 0";

        public AdicionarItemPedidoCommandValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdClienteErroMsg);

            RuleFor(c => c.PedidoItemId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdProdutoErroMsg);

            RuleFor(c => c.NomePedidoItem)
                .NotEmpty()
                .WithMessage(NomeErroMsg);

            RuleFor(c => c.QuantidadePedidoItem)
                .GreaterThan(0)
                .WithMessage(QtdMinErroMsg)
                .LessThanOrEqualTo(Pedido.MAX_UNIDADES_ITEM)
                .WithMessage(QtdMaxErroMsg);

            RuleFor(c => c.ValorUnitarioPedidoItem)
                .GreaterThan(0)
                .WithMessage(ValorErroMsg);
        }
    }
}
