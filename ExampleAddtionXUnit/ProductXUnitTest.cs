using ExampleAddition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExampleAddition
{

    public class ProductXUnitTest
    {
        [Fact]
        public void GetProductPrice_InputPlatinumCustomer_ReturnPriceWith20Discout()
        {
            Product product = new Product() { Price = 50 };

            var result = product.GetPrice(new Customer() { IsPlatium = true });

            Assert.Equal(40, result);
        }


    }
}
