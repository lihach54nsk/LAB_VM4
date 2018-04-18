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

        decimal accuracy = 0.0000001m;

        decimal Abs(decimal val) => val > 0 ? val : -val;

        [TestMethod]
        public void DerivativeTest1()
        {
            var validValue = 1.0m;
            foreach (var (x, y) in Derivative.FindDerivative(testData1))
                if (Abs(y - validValue) > accuracy) Assert.Fail();
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            var validValue = 1.0m;
            foreach (var (x, y) in Derivative.FindDerivative(testData1, 0.1m))
                if (Abs(y - validValue) > accuracy) Assert.Fail();
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            var validValue = 1.0m;
            foreach (var (x, y) in Derivative.FindDerivative(testData1, 1.5m))
                if (Abs(y - validValue) > accuracy) Assert.Fail();
        }

        [TestMethod]
        public void DerivativeTestTwoDegree1()
        {
            var validValue = 0;
            foreach (var (x, y) in Derivative.FindDerivative(testData1, 2))
                if (Abs(y - validValue) > accuracy) Assert.Fail(y.ToString());
        }

        [TestMethod]
        public void DerivativeTestTwoDegree2()
        {
            var validValue = 0;
            foreach (var (x, y) in Derivative.FindDerivative(testData1, 2, 0.01m))
                if (Abs(y - validValue) > accuracy) Assert.Fail(y.ToString());
        }

        [TestMethod]
        public void DerivativeTestTwoDegree3()
        {
            var validValue = 0;
            foreach (var (x, y) in Derivative.FindDerivative(testData1, 2, 1.5m))
                if (Abs(y - validValue) > accuracy) Assert.Fail(y.ToString());
        }
    }
}
