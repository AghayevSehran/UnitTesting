using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleAddition
{
    public class Customer
    {
        public int Discount = 15;
        public int OrderTotal { get; set; }
        public string GreetMessage { get; set; }
        public bool IsPlatium { get; set; }
        public Customer()
        {
            IsPlatium = false;
        }
        public string CombineNames(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("Empty First Name");
            }
            GreetMessage = $"Hello, {firstName} {lastName}";
            Discount = 20;
            return GreetMessage;
        }

        public CustomerType GetCustomer()
        {
            if (OrderTotal < 100)
            {
                return new BasicCustomer();
            }
            return new PlantiumCustomer();
        }
    }

    public class CustomerType { }
    public class BasicCustomer : CustomerType { }
    public class PlantiumCustomer : CustomerType { }
}
