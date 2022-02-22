using ExampleAddition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Example
{

    public class CalculatorXUnitTest
    {
        [Fact]
        public void AddNumbers_InputTwoInt_GetCirrectAddion()
        {
            Calculator calculator = new();

            var result = calculator.Add(1, 2);

            Assert.Equal(3, result);
        }
        [Theory]
        [InlineData(11.1, 0.9)]
        public void AddNumbers_InputTwodouble_GetCirrectAddion(double a, double b)
        {
            Calculator calculator = new();

            var result = calculator.AddDoubles(a, b);

            Assert.Equal(12, result, 0);
        }
        //[Fact]
        //public void IsOddChecker_InputEvenNumber_ReturnFalse()
        //{
        //    Calculator calculator = new();

        //    var isOdd = calculator.IsOddNumber(2018);

        //    //Assert.IsFalse(isOdd);
        //    Assert.That(isOdd, Is.EqualTo(false));
        //}

        //[Theory]
        //[InlineData(11)]
        //[InlineData(13)]
        //public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
        //{
        //    Calculator calculator = new();

        //    var isOdd = calculator.IsOddNumber(a);

        //    //Assert.IsFalse(isOdd);
        //    Assert.That(isOdd, Is.EqualTo(true));
        //}

        //[Theory]
        //[InlineData(10, ExpectedResult = false)]
        //[InlineData(13, ExpectedResult = true)]
        //public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int a)
        //{
        //    Calculator calculator = new();
        //    return calculator.IsOddNumber(a);

        //}

        [Fact]
        public void OddRanger_InputMinMax_ReturnOddNumbersCollection()
        {
            Calculator calculator = new();

            List<int> expectedResult = new() { 5, 7, 9 };
            var r = calculator.GetOddRange(5, 10);

            Assert.Equal(expectedResult, r);
            Assert.Contains(7, r);
            Assert.NotEmpty(r);
            Assert.Equal(3, r.Count);
            Assert.DoesNotContain(17, r);
            Assert.Equal(r.OrderBy(o => o), r);
            //Assert.That(r, Is.Unique);

        }
    }
}
