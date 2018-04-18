using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_VM4
{
    public static class DerivativeDouble
    {
        /// <summary>
        /// Ищет производную по заданному набору значений, используя указанный шаг
        /// </summary>
        /// <param name="input">Таблица значений XY</param>
        /// <returns></returns>
        public static IEnumerable<(double x, double y)> FindDerivative(double[,] input, double step)
        {
            if (input.GetLength(0) != 2) throw new FormatException("Length error");
            if (step > input[0, 1] - input[0, 0]) throw new FormatException("Step error " + step.ToString() + ' ' + (input[0, 1] - input[0, 0]).ToString());

            var length = input.GetLength(1);

            var lagKoeff = InterpolationDouble.GetLagrange(input);

            for (var x = step + input[0, 0]; x < input[0, length - 1]; x += step)
            {
                var y0 = InterpolationDouble.InterpolateLagrange(lagKoeff, x - step);
                var y1 = InterpolationDouble.InterpolateLagrange(lagKoeff, x);
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
        public static IEnumerable<(double x, double y)> FindDerivative(double[,] input)
        {
            return FindDerivative(input, (input[0, 1] - input[0, 0]) / 2);
        }

        /// <summary>
        /// Ищет производную указанного порядка с указанным шагом по заданной таблице значений XY
        /// </summary>
        /// <param name="input">Таблица значений XY</param>
        /// <param name="degree">Порядок</param>
        /// <param name="step">Шаг</param>
        public static IEnumerable<(double x, double y)> FindDerivative(double[,] input, int degree, double step)
        {
            if (degree < 1) throw new FormatException(degree.ToString());

            if (degree == 1) return FindDerivative(input, step);

            var poolInput = input;
            var poolList = new List<(double x, double y)>();

            for (var i = 0; i < degree - 1; i++)
            {

                foreach (var a in FindDerivative(poolInput, step))
                {
                    poolList.Add(a);
                }

                poolInput = new double[2, poolList.Count];

                for (var j = 0; j < poolList.Count; j++)
                {
                    (poolInput[0, j], poolInput[1, j]) = (poolList[j].x, poolList[j].y);
                }
                step /= 2.0;
                poolList.Clear();
            }

            return FindDerivative(poolInput, step);
        }

        /// <summary>
        /// Ищет производную указанного порядка по заданной таблице значений XY со стандартным шагом
        /// </summary>
        /// <param name="input">Таблица значений XY</param>
        /// <param name="degree">Порядок</param>
        public static IEnumerable<(double x, double y)> FindDerivative(double[,] input, int degree)
        {
            return FindDerivative(input, degree, (input[0, 1] - input[0, 0]) / 2);
        }

    }
}
