using ExampleAddition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExampleAddition
{

    public class GradingCalculatorXUnitTest
    {
        GradingCalculator gradingCalculator;

        public GradingCalculatorXUnitTest()
        {
            gradingCalculator = new GradingCalculator();
        }

        [Fact]
        public void GetGrade_InputScopeMoreThan90AndAttendancePercentageMoreThan70_ReturnA()
        {
            gradingCalculator.Score = 91;
            gradingCalculator.AttendancePercentage = 71;

            var result = gradingCalculator.GetGrade();

            Assert.Equal("A", result);
        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 65, "B")]
        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        public void GetGrade_InputScopeAndAttendancePercentage_ReturnABCDF
           (int score, int attendancePercentage, string expectedResult)
        {
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendancePercentage;

            var result = gradingCalculator.GetGrade();
            Assert.Equal(expectedResult, result);

        }
    }
}
