using Moq;
using Xunit;
using XUnitTest.App;

namespace XUnitTest.Test.XUnitTest.App.Test
{
    public class CalculatorTest
    {

        public Calculator Calculator { get; set; }
        public Mock<ICalculatorService> calculatorServiceMock { get; set; }

        public CalculatorTest()
        {
            calculatorServiceMock = new Mock<ICalculatorService>();
            Calculator = new Calculator(calculatorServiceMock.Object);
        }


        //parametre almayan test metotlarında[Fact] attribute u kullanılır.

        [Fact]
        public void AddTest_ReturnTotalValue_WhenAandBIsDifferentValue()
        {
            //Arrange
            int a = 10;
            int b = 20;
            int exceptedTotal = a + b;


            //Act
            calculatorServiceMock.Setup(c => c.Add(a, b)).Returns(exceptedTotal);
            int actualTotal = Calculator.Add(a, b);
            //Assert
            Assert.Equal(exceptedTotal, actualTotal);
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
            Assert.Equal(0, total);
        }
        //parametre alan test metotlarında [Theory] ve [InlineData] metotları birlikte kullanılır.

        [Theory]
        [InlineData(2, 5, 7)]
        [InlineData(10, 2, 12)]

        public void TestWithParameters(int a, int b, int expectedTotal)
        {
            var actualTotal = Calculator.Add(a, b);
            Assert.Equal(expectedTotal, actualTotal);
        }

        [Theory]
        [InlineData(1, 5, 5)]

        public void Multip_ReturnMultipleValue_WhenAIsDifferentFromZero(int a, int b, int expectedValue)
        {
            int actualMultip = 0;
            calculatorServiceMock.Setup(x => x.Multip(It.IsAny<int>(), It.IsAny<int>())).Callback<int, int>((x, y) => actualMultip = x * y);
            //inline datadan gelen değerler;
            Calculator.Multip(a, b);
            Assert.Equal(expectedValue, actualMultip);

            //manuel girilen değerler
            //inline datadan gelen değerler;
            Calculator.Multip(8, 4);
            Assert.Equal(32, actualMultip);
        }

        [Theory]
        [InlineData(0, 5)]

        public void Multip_ReturnExceptionMessage_WhenAIsZero(int a, int b)
        {
            calculatorServiceMock.Setup(x => x.Multip(a, b)).Throws(new Exception("a 0 olamaz"));
            Exception ex = Assert.Throws<Exception>(() => Calculator.Multip(a, b));
            Assert.Equal("a 0 olamaz", ex.Message);
        }
    }
}
