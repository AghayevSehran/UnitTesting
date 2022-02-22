using ExampleAddition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Example
{

    public class CustomerXUintTests
    {
        private Customer customer;

        public CustomerXUintTests()
        {
            customer = new Customer();
        }


        [Fact]
        public void CombineNames_InputFisrtAndLastName_ReturnCombinenResult()
        {

            customer.CombineNames("Ben", "Spark");

            Assert.Equal("Hello, Ben Spark", customer.GreetMessage);
            Assert.Equal("Hello, Ben Spark", customer.GreetMessage);
            Assert.Contains(",", customer.GreetMessage);
            Assert.StartsWith("Hello", customer.GreetMessage);
            Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", customer.GreetMessage);
        }


        [Fact]
        public void GreetMessages_CombineNamesMethodNotCalled_ReturnNull()
        {

            Assert.Null(customer.GreetMessage);
            Assert.True(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Fact]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = customer.Discount;
            Assert.InRange(result, 10, 25);
        }

        [Fact]
        public void GreetMessages_EmptyFisrtName_ThroüsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => customer.CombineNames("", "Spark"));

            Assert.Equal("Empty First Name", exception.Message);

            Assert.Contains("Empty First Name", exception.Message);



            Assert.Throws<ArgumentException>(() => customer.CombineNames("", "Spark"));

        }

        [Fact]
        public void GetCustomer_CreateCustomerWithLessThan100_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomer();
            Assert.IsType<BasicCustomer>(result);
        }

        [Fact]
        public void GetCustomer_CreateCustomerWithMoreThan100_ReturnBasicCustomer()
        {
            customer.OrderTotal = 190;
            var result = customer.GetCustomer();
            Assert.IsType<PlantiumCustomer>(result);
        }

    }
}
