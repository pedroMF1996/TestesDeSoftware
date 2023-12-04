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
    }
}
