using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB_VM4;
using Lab2_VM;

namespace Lab3_VM
{
    public static class Interpolation
    {
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

        ////////////////////
        //Попробуй сделать это через перечисления

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