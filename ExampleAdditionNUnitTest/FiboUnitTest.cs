using ExampleAddition;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleAddition
{
    [TestFixture]
    public class FiboUnitTest
    {

        [Test]
        public void GetFiboSeries_Input_ReturnCollection()
        {

            Fibo fibo = new();
            List<int> list = new() { 0, 1, 1, 2, 3 };

            var result = fibo.GetFiboSeries();

            Assert.That(result, Is.EquivalentTo(list));
        }

        [Test]
        public void GetFiboSeries_InputRange1_ReturnList()
        {

            Fibo fibo = new();
            fibo.Range = 1;
            List<int> list = new() { 0 };

            var result = fibo.GetFiboSeries();

            Assert.That(result, Is.Not.Empty);

            Assert.That(result.Count, Is.AtLeast(1));

            Assert.That(result, Is.Ordered);

            Assert.That(result, Is.EquivalentTo(list));
        }

        public void GetFiboSeries_Input6_ReturnCollection()
        {

            Fibo fibo = new();
            fibo.Range = 6;
            List<int> list = new() { 0, 1, 1, 2, 3, 5 };

            var result = fibo.GetFiboSeries();

            Assert.That(result, Does.Contain(3));

            Assert.That(result.Count, Is.EqualTo(6));

            Assert.That(result.Count, Has.No.Member(4));

            Assert.That(result, Is.EquivalentTo(list));
        }
    }
}
