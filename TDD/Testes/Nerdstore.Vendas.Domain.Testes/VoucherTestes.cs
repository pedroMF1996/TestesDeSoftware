
using Nerdstore.Vendas.Domain.Entidades;

namespace Nerdstore.Vendas.Domain.Testes
{
    public class VoucherTestes
    {
        [Fact(DisplayName = "Validar Voucher Tipo Valor Valido")]
        [Trait("Categoria", "Venda - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("PROMO-15-REAIS", 15, 0, TipoDesconto.Valor, 1, DateTime.Now.AddDays(1), true, false);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar Voucher Tipo Valor Invalido")]
        [Trait("Categoria", "Venda - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, TipoDesconto.Valor, 0, DateTime.Now, false, true);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            var errors = result.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicavelValidation.ValorDescontoErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, errors);
        }

        [Fact(DisplayName = "Validar Voucher Tipo Porcentagem Valido")]
        [Trait("Categoria", "Venda - Voucher")]
        public void Voucher_ValidarVoucherTipoPorcentagem_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("PROMO-15-REAIS", 0, 15, TipoDesconto.Porcentagem, 1, DateTime.Now.AddDays(1), true, false);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar Voucher Tipo Porcentagem Invalido")]
        [Trait("Categoria", "Venda - Voucher")]
        public void Voucher_ValidarVoucherTipoPorcentagem_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, TipoDesconto.Porcentagem, 0, DateTime.Now, false, true);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            var errors = result.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicavelValidation.PercentualDescontoErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, errors);
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, errors);
        }


    }
}
