using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab3_VM;

namespace LAB_VM4
{
    public static class Derivative
    {
        public static decimal[] FindDerivative(int N, decimal[,] input, decimal[] inputX, decimal[] inputY)
        {
            decimal[] derivative = new decimal[2 * N]; // массив производных
            decimal[] Xarray = new decimal[2 * N];

            decimal h = (inputX[1] - inputX[0]) / 2; // шаг
            decimal n = 1;

            Xarray[0] = inputX[0];

            for (int i = 1; i < 2 * N; i++) { Xarray[i] = h * n; n++; }

            for (int i = 2 * N - 1; i == 1; i--)
                derivative[i] = (Interpolation.InterpolateLagrange(input, Xarray[i]) - Interpolation.InterpolateLagrange(input, Xarray[i - 1])) / h;

            derivative[0] = inputY[0];

            return derivative;
        }

        /// <summary>
        /// Ищет производную по заданному набору значений, используя указанный шаг
        /// </summary>
        /// <param name="input">Таблица значений XY</param>
        /// <returns></returns>
        public static IEnumerable<(decimal x,decimal y)> FindDerivative(decimal[,] input, decimal step)
        {
            if (input.GetLength(0) != 2) throw new FormatException("Length error");
            if (step > input[0, 1] - input[0, 0]) throw new FormatException("Step error "+step.ToString()+' '+(input[0, 1] - input[0, 0]).ToString());

            var length = input.GetLength(1);

            var lagKoeff = Interpolation.GetLagrange(input);

            for (var x = step + input[0, 0] ; x < input[0, length - 1]; x += step)
            {
                var y0 = Interpolation.InterpolateLagrange(lagKoeff, x-step);
                var y1 = Interpolation.InterpolateLagrange(lagKoeff, x);
                var derivative = (y1 - y0) / step;
                yield return (x, derivative);
            }
        }

        /// <summary>
        /// Ищет производную по заданному набору значений, используя шаг 
        /// равный половине шага таблицы
        /// </summary>
        /// <param name="input">Таблица значений XY</param>
        /// <returns></returns>
        public static IEnumerable<(decimal x, decimal y)> FindDerivative(decimal[,] input)
        {
            return FindDerivative(input, (input[0,1] - input[0,0]) / 2);
        }

        /// <summary>
        /// Ищет производную указанного порядка с указанным шагом по заданной таблице значений XY
        /// </summary>
        /// <param name="input">Таблица значений XY</param>
        /// <param name="degree">Порядок</param>
        /// <param name="step">Шаг</param>
        public static IEnumerable<(decimal x, decimal y)> FindDerivative(decimal[,] input, int degree, decimal step)
        {
            if (degree < 1) throw new FormatException(degree.ToString());

            if (degree == 1) return FindDerivative(input, step);

            var poolInput = input;
            var poolList = new List<(decimal x, decimal y)>();

            for (var i = 0; i < degree - 1; i++)
            {

                foreach (var a in FindDerivative(poolInput, step))
                {
                    poolList.Add(a);
                }

                poolInput = new decimal[2, poolList.Count];

                for (var j = 0; j < poolList.Count; j++)
                {
                    (poolInput[0, j], poolInput[1, j]) = (poolList[j].x, poolList[j].y);
                }
                step /= 2.0m;
                poolList.Clear();
            }

            return FindDerivative(poolInput, step);
        }

        /// <summary>
        /// Ищет производную указанного порядка по заданной таблице значений XY со стандартным шагом
        /// </summary>
        /// <param name="input">Таблица значений XY</param>
        /// <param name="degree">Порядок</param>
        public static IEnumerable<(decimal x, decimal y)> FindDerivative(decimal[,] input, int degree)
        {
            return FindDerivative(input, degree, (input[0, 1] - input[0, 0]) / 2);
        }
    }
}