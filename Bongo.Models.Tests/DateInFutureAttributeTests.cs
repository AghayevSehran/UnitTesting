using Bongo.Models.ModelValidations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Models.Tests
{
    [TestFixture]
    public class DateInFutureAttributeTests
    {
        [TestCase(100, ExpectedResult = true)]
        [TestCase(-100, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        public bool DateValidator_InputExpectedDateRange_DateValidity(int time)
        {
            DateInFutureAttribute dateInFutureAttribute = new(
                () => DateTime.Now);

            return dateInFutureAttribute.IsValid
                (DateTime.Now.AddSeconds(time));

        }

        [Test]
        public void DateValidator_InputNotValideDate_ReturnsErrorMessage()
        {
            var result = new DateInFutureAttribute();

            Assert.AreEqual("Date must be in the future", result.ErrorMessage);

        }

    }
}
