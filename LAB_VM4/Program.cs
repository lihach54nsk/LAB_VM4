using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB_VM4
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            decimal[,] testData1 =
            {
                {0, 2, 4},
                {0, 2, 4}
            };

            var enumer = Derivative.FindDerivative(testData1).GetEnumerator();
            while(enumer.MoveNext())
            {
                var (x, y) = enumer.Current;
                Console.WriteLine("{0}, {1}", x, y);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
