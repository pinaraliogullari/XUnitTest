using Xunit;
using XUnitTest.App;

namespace XUnitTest.Test
{
	public class CalculatorTest
	{

		public Calculator Calculator { get; set; }
        public CalculatorTest()
        {
            this.Calculator = new Calculator();
        }


        //parametre almayan test metotlarında[Fact] attribute u kullanılır.

        [Fact]
		public void AddTest_ReturnTotalValue_WhenAandBIsDifferentValue()
		{
			//Arrange
			int a = 10;
			int b = 20;
			//Act
			var total= Calculator.Add(a, b);
			//Assert
			Assert.Equal<int>(30, total);
		}

		[Fact]
		public void AddTest_ReturnZero_WhenAandBIsSameValue()
		{
			//Arrange
			int a = 10;
			int b = 10;
			//Act
			var total = Calculator.Add(a, b);
			//Assert
			Assert.Equal<int>(0, total);
		}
		//parametre alan test metotlarında [Theory] ve [InlineData] metotları birlikte kullanılır.

		[Theory]
		[InlineData(2,5,7)]
		[InlineData(10,2,12)]

		public void TestWithParameters(int a, int b, int expectedTotal)
		{
			var actualTotal= Calculator.Add(a, b);
			Assert.Equal(expectedTotal, actualTotal);
		}
	}
}
