using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_VM4
{
    class InterpolationDouble
    {

        /// <summary>
        /// Возвращает массив коэффициентов лагранжа
        /// </summary>
        /// <param name="inputTable">Таблица (x,y)</param>
        public static double[] GetLagrange(double[,] inputTable)
        {
            if (inputTable.GetLength(0) != 2) throw new FormatException();

            var length = inputTable.GetLength(1);

            double[] outArray = null;

            for (int i = 0; i < length; i++)
            {
                var value = GetLagrangeKoeff(inputTable, i);

                if (outArray == null)
                {
                    outArray = value;
                }
                else
                {
                    for (int j = 0; j < length; j++)
                    {
                        outArray[j] += value[j];
                    }
                }
            }

            return outArray;
        }

        /// <summary>
        /// Принимает начальную таблицу значений и x, возвращает y
        /// </summary>
        public static double InterpolateLagrange(double[,] inputTable, double x)
            => InterpolateLagrange(GetLagrange(inputTable), x);

        /// <summary>
        /// Принимает коэффициенты лагранжа и x, возвращает y
        /// </summary>
        public static double InterpolateLagrange(double[] lagrangeKoeffs, double x)
        {
            var result = 0.0;
            var count = lagrangeKoeffs.Length;

            for (int i = 0; i < count; i++)
                result += lagrangeKoeffs[count - i - 1] * Pow(x, i);

            return result;
        }

        private static double Pow(double value, int n)
        {
            var pool = 1.0;

            for (int i = 0; i < n; i++)
                pool *= value;

            return pool;
        }

        private static double[] GetLagrangeKoeff(double[,] inputTable, int i)
        {
            if (inputTable.GetLength(0) != 2) throw new FormatException();
            var length = inputTable.GetLength(1);

            var outArray = new double[length];

            var koeffNumber = 0;
            outArray[0] = 1;

            for (int j = 0; j < length; j++)
            {
                if (i == j) continue;

                koeffNumber++;

                for (int k = koeffNumber - 1; k >= 0; k--)
                {
                    outArray[k + 1] -= outArray[k] * inputTable[0, j];
                }
            }

            var koeff = inputTable[1, i];

            for (int j = 0; j < length; j++)
            {
                if (i == j) continue;

                koeff /= (inputTable[0, i] - inputTable[0, j]);
            }

            for (int j = 0; j < length; j++)
            {
                outArray[j] *= koeff;
            }

            return outArray;
        }


    }
}
