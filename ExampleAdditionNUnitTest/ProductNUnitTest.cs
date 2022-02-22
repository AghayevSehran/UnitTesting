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
    public class ProductNUnitTest
    {
        [Test]
        public void GetProductPrice_InputPlatinumCustomer_ReturnPriceWith20Discout()
        {
            Product product = new Product() { Price = 50 };

            var result = product.GetPrice(new Customer() { IsPlatium = true });

            Assert.That(result, Is.EqualTo(40));
        }


    }
}
