using ExampleAddition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExampleAddition
{

    public class FiboXnitTest
    {

        [Fact]
        public void GetFiboSeries_Input_ReturnCollection()
        {

            Fibo fibo = new();
            List<int> list = new() { 0, 1, 1, 2, 3 };

            var result = fibo.GetFiboSeries();

            Assert.Equal(list, result);
        }

        [Fact]
        public void GetFiboSeries_InputRange1_ReturnList()
        {

            Fibo fibo = new();
            fibo.Range = 1;
            List<int> list = new() { 0 };

            var result = fibo.GetFiboSeries();

            Assert.NotEmpty(result);

            Assert.Equal(result.OrderBy(p => p), result);

            Assert.Equal(list, result);
        }
        [Fact]
        public void GetFiboSeries_Input6_ReturnCollection()
        {

            Fibo fibo = new();
            fibo.Range = 6;
            List<int> list = new() { 0, 1, 1, 2, 3, 5 };

            var result = fibo.GetFiboSeries();

            Assert.Contains(3, result);

            Assert.Equal(6, result.Count);

            Assert.DoesNotContain(4, result);

            Assert.Equal(list, result);
        }
    }
}
