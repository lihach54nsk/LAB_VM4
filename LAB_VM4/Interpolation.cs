using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2_VM;

namespace Lab3_VM
{
    public static class Interpolation
    {
        /// <summary>
        /// Возвращает массив коэффициентов лагранжа
        /// </summary>
        /// <param name="inputTable">Таблица (x,y)</param>
        public static decimal[] GetLagrange(decimal[,] inputTable)
        {
            if (inputTable.GetLength(0) != 2) throw new FormatException();

            var length = inputTable.GetLength(1);

            decimal[] outArray = null;

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
        public static decimal InterpolateLagrange(decimal[,] inputTable, decimal x) 
            => InterpolateLagrange(GetLagrange(inputTable), x);

        /// <summary>
        /// Принимает коэффициенты лагранжа и x, возвращает y
        /// </summary>
        public static decimal InterpolateLagrange(decimal[] lagrangeKoeffs, decimal x)
        {
            var result = 0m;
            var count = lagrangeKoeffs.Length;

            for (int i = 0; i < count; i++)
                result += lagrangeKoeffs[count - i - 1] * Pow(x, i);

            return result;
        }

        private static decimal Pow(decimal value, int n)
        {
            var pool = 1m;

            for (int i = 0; i < n; i++)
                pool *= value;

            return pool;
        }


        //Сплайны

        private static decimal[] GetLagrangeKoeff(decimal[,] inputTable, int i)
        {
            if (inputTable.GetLength(0) != 2) throw new FormatException();
            var length = inputTable.GetLength(1);

            var outArray = new decimal[length];

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

        /// <summary>
        /// Возвращает массив коеффициентов m, без вывода непосредственно коэффициентов сплайна
        /// </summary>
        /// <param name="inputTable"></param>
        /// <returns></returns>
        public static decimal[] GetSplineCube(decimal[,] inputTable)
        {
            var splineMatrix = GetSplineMatrix(inputTable);
            for (int i = 0; i < splineMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < splineMatrix.GetLength(1); j++)
                {
                    Console.Write(splineMatrix[i, j].ToString() + ' ');
                }
                Console.WriteLine();
            }

            var mKoeffArray = Sweep.SweepMatrix(splineMatrix);

            return mKoeffArray;
        }

        public static decimal[,] GetSplineCubeFull(decimal[,] inputTable)
        {
            var mKoeffArray = GetSplineCube(inputTable);
            var length = mKoeffArray.Length+1;
            var outArray = new decimal[length, 4];

            decimal H(int i) => inputTable[0, i + 1] - inputTable[0, i];
            decimal D(int i) => (inputTable[1, i + 1] - inputTable[1, i]) / H(i);
            decimal M(int i) => ((i == 0) || (i == length)) ? 0 : mKoeffArray[i - 1];

            for (int i = 0; i < length; i++)
            {
                outArray[i, 3] = inputTable[1, i];
                outArray[i, 2] = D(i) - (H(i) * (2 * M(i) + M(i + 1))) / 6m;
                outArray[i, 1] = M(i) / 2m;
                outArray[i, 0] = (M(i + 1) - M(i)) / (6 * H(i));
            }

            return outArray;
        }

        public static decimal InterpolateSpline(decimal[,] inputTable, decimal x)
        {
            var splineKoeffs = GetSplineCubeFull(inputTable);
            var lineNum = -1;

            for (int i = 0; i < inputTable.GetLength(1)-1; i++)
            {
                if (inputTable[0, i] <= x)
                    lineNum = i;
            }

            if (lineNum < 0) throw new Exception("Wrong input");

            decimal S(int k) => splineKoeffs[lineNum, k];
            decimal w = x - inputTable[0, lineNum];

            return ((S(0) * w + S(1)) * w + S(2)) * w + S(3);
        }

        private static decimal[,] GetSplineMatrix(decimal[,] inputTable)
        {
            decimal H(int i) => inputTable[0, i + 1] - inputTable[0, i];

            decimal D(int i) => (inputTable[1, i + 1] - inputTable[1, i]) / H(i);

            var hight = inputTable.GetLength(0);
            var length = inputTable.GetLength(1) - 2;

            if (hight != 2) throw new FormatException();

            var outputTable = new decimal[length, length + 1];

            for (var i = 0; i < length; i++)
            {
                if (i != 0)
                    outputTable[i, i - 1] = H(i);

                if (i != length - 1)
                    outputTable[i, i + 1] = H(i + 1);

                outputTable[i, i] = 2 * (H(i) + H(i + 1));

                outputTable[i, length] = 6 * (D(i + 1) - D(i));
            }

            return outputTable;
        }
    }
}