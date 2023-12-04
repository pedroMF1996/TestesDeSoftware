using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Testes
{
    public class CalculadoraTestes
    {
        [Fact]
        public void Calculadora_Somar_RetornarResuldadoSoma()
        {
            //Arrange
            Calculadora calculadora = new();

            //Act
            var restultado = calculadora.Somar(2, 2);

            //Assert
            Assert.Equal(4, restultado);
        }

        [Theory]
        [InlineData(2,2,4)]
        [InlineData(1,2,3)]
        [InlineData(2,7,9)]
        [InlineData(2,5,7)]
        [InlineData(2,3,5)]
        public void Calculadora_Somar_RetornarValorSoma(double x, double y, double expectedResult)
        {
            //Arrange
            Calculadora calculadora = new();

            //Act
            var restultado = calculadora.Somar(x, y);

            //Assert
            Assert.Equal(expectedResult, restultado);
        }
    }
}
