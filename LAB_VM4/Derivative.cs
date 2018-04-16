using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab3_VM;

namespace LAB_VM4
{
    public class Derivative
    {
        public decimal[] FindDerivative(int N, decimal[,] input, decimal[] inputX, decimal[] inputY)
        {
            decimal[] derivative = new decimal[N];

            decimal h = (inputX[1] - inputX[0]) / 2; // шаг

            for (int i = N - 1; i == 0; i--)
            {
                derivative[i] = (inputY[i] - Interpolation.GetLagrange(input)[i - 1]) / h;
            }

            return derivative;
        }
    }
}