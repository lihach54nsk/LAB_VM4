using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LAB_VM4;

namespace Lab4_VM_Test
{
    [TestClass]
    public class DerivativeTestDouble
    {
        double[,] testData1 =
           {
                {0, 2, 4},
                {0, 2, 4}
            };

        double accuracy = 0.001;

        decimal Abs(decimal val) => val > 0 ? val : -val;

        [TestMethod]
        public void DerivativeDoubleTest1()
        {
            var validValue = 1.0;
            foreach (var (x, y) in DerivativeDouble.FindDerivative(testData1))
                Assert.AreEqual(y, validValue, accuracy);
        }

        [TestMethod]
        public void DerivativeDoubleTest2()
        {
            var validValue = 1.0;
            foreach (var (x, y) in DerivativeDouble.FindDerivative(testData1, 0.1))
                Assert.AreEqual(y, validValue, accuracy);
        }

        [TestMethod]
        public void DerivativeDoubleTest3()
        {
            var validValue = 1.0;
            foreach (var (x, y) in DerivativeDouble.FindDerivative(testData1, 1.5))
                Assert.AreEqual(y, validValue, accuracy);
        }

        [TestMethod]
        public void DerivativeDoubleTestTwoDegree1()
        {
            var validValue = 0;
            foreach (var (x, y) in DerivativeDouble.FindDerivative(testData1, 2))
                Assert.AreEqual(y, validValue, accuracy);
        }

        [TestMethod]
        public void DerivativeDoubleTestTwoDegree2()
        {
            var validValue = 0;
            foreach (var (x, y) in DerivativeDouble.FindDerivative(testData1, 2, 0.01))
                Assert.AreEqual(y, validValue, accuracy);
        }

        [TestMethod]
        public void DerivativeDoubleTestTwoDegree3()
        {
            var validValue = 0;
            foreach (var (x, y) in DerivativeDouble.FindDerivative(testData1, 2, 1.5))
                Assert.AreEqual(y, validValue, accuracy);
        }
    }
}


