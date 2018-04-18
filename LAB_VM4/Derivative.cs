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
            decimal[] derivative = new decimal[2 * N]; // массив производных
            decimal[] Xarray = new decimal[2 * N];

            decimal h = (inputX[1] - inputX[0]) / 2; // шаг
            decimal n = 1;

            Xarray[0] = inputX[0];

            for (int i = 1; i < 2 * N; i++) { Xarray[i] = h * n; n++; }

            for (int i = 2 * N - 1; i == 0; i--)
            {
                derivative[i] = (inputY[i] - Interpolation.InterpolateLagrange(input, Xarray[i]) / h);
            }

            return derivative;
        }
    }
}