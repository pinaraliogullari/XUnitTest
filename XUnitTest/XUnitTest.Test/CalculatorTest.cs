using Xunit;
using XUnitTest.App;

namespace XUnitTest.Test
{
	public class CalculatorTest
	{
		[Fact]
		public void AddTest()
		{
			//Arrange
			int a = 10;
			int b = 20;
			Calculator calculator = new();
			//Act
			var total= calculator.Add(a, b);
			//Assert
			Assert.Equal<int>(30, total);
		}
	}
}
