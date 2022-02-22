using ExampleAddition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExampleAdditionMSTest
{
    [TestClass]
    public class CaculatorMSTests
    {
        [TestMethod]
        public void AddNumbers_InputTwoInt_GetCirrectAddion()
        {
            Calculator calculator = new();

            var result = calculator.Add(1, 2);

            Assert.AreEqual(3, result);
        }
    }
}