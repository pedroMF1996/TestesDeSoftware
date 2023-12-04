using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class Calculadora
    {
        public double Somar(double x, double y)
        {
            return x + y;
        }

        public double Dividir(double x, double y)
        {
            if (y == 0) throw new DivideByZeroException();
            return x / y;
        }
    }
}
