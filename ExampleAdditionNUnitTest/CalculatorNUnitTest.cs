using ExampleAddition;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    [TestFixture]
    public class CalculatorNUnitTest
    {
        [Test]
        public void AddNumbers_InputTwoInt_GetCirrectAddion()
        {
            Calculator calculator = new();

            var result = calculator.Add(1, 2);

            Assert.AreEqual(3, result);
        }
        [Test]
        [TestCase(11.1, 0.9)]
        [TestCase(12.1, 0.9)]
        public void AddNumbers_InputTwodouble_GetCirrectAddion(double a, double b)
        {
            Calculator calculator = new();

            var result = calculator.AddDoubles(a, b);

            Assert.AreEqual(12, result, 1);
        }
        [Test]
        public void IsOddChecker_InputEvenNumber_ReturnFalse()
        {
            Calculator calculator = new();

            var isOdd = calculator.IsOddNumber(2018);

            //Assert.IsFalse(isOdd);
            Assert.That(isOdd, Is.EqualTo(false));
        }

        [Test]
        [TestCase(11)]
        [TestCase(13)]
        public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        {
            Calculator calculator = new();

            var isOdd = calculator.IsOddNumber(a);

            //Assert.IsFalse(isOdd);
            Assert.That(isOdd, Is.EqualTo(true));
        }

        [Test]
        [TestCase(10, ExpectedResult = false)]
        [TestCase(13, ExpectedResult = true)]
        public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int a)
        {
            Calculator calculator = new();
            return calculator.IsOddNumber(a);

        }

        [Test]
        public void OddRanger_InputMinMax_ReturnOddNumbersCollection()
        {
            Calculator calculator = new();

            List<int> expectedResult = new() { 5, 7, 9 };
            var r = calculator.GetOddRange(5, 10);

            Assert.That(r, Is.EquivalentTo(expectedResult));
            Assert.That(r, Does.Contain(7));
            Assert.That(r, Is.Not.Empty);
            Assert.Multiple(() =>
            {
                Assert.That(r.Count, Is.EqualTo(3));
                Assert.That(r, Has.No.Member(17));
                Assert.That(r, Is.Ordered);
                Assert.That(r, Is.Unique);
            });

        }
    }
}
