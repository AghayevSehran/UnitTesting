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
    public class GradingCalculatorNUnitTest
    {
        GradingCalculator gradingCalculator;
        [SetUp]
        public void SetUp()
        {
            gradingCalculator = new GradingCalculator();
        }

        [Test]
        public void GetGrade_InputScopeMoreThan90AndAttendancePercentageMoreThan70_ReturnA()
        {
            gradingCalculator.Score = 91;
            gradingCalculator.AttendancePercentage = 71;

            var result = gradingCalculator.GetGrade();

            Assert.That(result, Is.EqualTo("A"));
        }

        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        public string GetGrade_InputScopeAndAttendancePercentage_ReturnABCDF
            (int score, int attendancePercentage)
        {
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendancePercentage;

            return gradingCalculator.GetGrade();
        }
    }
}
