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
    public class CustomerNUintTests
    {
        private Customer customer;
        [SetUp]
        public void SetUp()
        {
            customer = new Customer();
        }


        [Test]
        public void CombineNames_InputFisrtAndLastName_ReturnCombinenResult()
        {

            customer.CombineNames("Ben", "Spark");

            Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark"));
            Assert.AreEqual(customer.GreetMessage, "Hello, Ben Spark");
            Assert.That(customer.GreetMessage, Does.Contain(","));
            Assert.That(customer.GreetMessage, Does.StartWith("Hello"));
            Assert.That(customer.GreetMessage, Does.EndWith("Spark"));
            Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
        }


        [Test]
        public void GreetMessages_CombineNamesMethodNotCalled_ReturnNull()
        {

            Assert.IsNull(customer.GreetMessage);
            Assert.That(customer.GreetMessage, Is.Null);
        }

        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = customer.Discount;
            Assert.That(result, Is.InRange(10, 25));
        }

        [Test]
        public void GreetMessages_EmptyFisrtName_ThroüsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => customer.CombineNames("", "Spark"));

            Assert.AreEqual("Empty First Name", exception.Message);

            Assert.That(exception.Message,
               Does.Contain("Empty First Name"));

            Assert.That(() =>
                customer.CombineNames("", "Spark"), Throws.ArgumentException.With.Message.Contain("Empty First Name")
            );

            Assert.Throws<ArgumentException>(() => customer.CombineNames("", "Spark"));

            Assert.That(() => customer.CombineNames("", "Spark"),
                Throws.ArgumentException.With.Message.Contain("Empty First Name"));
        }

        [Test]
        public void GetCustomer_CreateCustomerWithLessThan100_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomer();
            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void GetCustomer_CreateCustomerWithMoreThan100_ReturnBasicCustomer()
        {
            customer.OrderTotal = 190;
            var result = customer.GetCustomer();
            Assert.That(result, Is.TypeOf<PlantiumCustomer>());
        }

    }
}
