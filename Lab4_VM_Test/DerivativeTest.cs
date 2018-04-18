using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LAB_VM4;

namespace Lab4_VM_Test
{
    [TestClass]
    public class DerivativeTest
    {
        decimal[,] testData1 =
            {
                {0, 2, 4},
                {0, 2, 4}
            };

        decimal Abs(decimal val) => val > 0 ? val : -val;

        [TestMethod]
        public void DerivativeTest1()
        {
            var accuracy = 0.001m;
            var validValue = 1.0m;
            foreach (var (x, y) in Derivative.FindDerivativeAlternative(testData1))
                if (Abs(y - validValue) > accuracy) Assert.Fail();
        }
    }
}
