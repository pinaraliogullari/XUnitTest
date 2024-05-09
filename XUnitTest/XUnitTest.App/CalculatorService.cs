namespace XUnitTest.App
{
	public class CalculatorService : ICalculatorService
	{
		public int Add(int a, int b)
		{
			if(a==b) return 0;
			return a + b;
		}

		public int Multip(int a, int b)
		{
			if (a == 0) throw new Exception("a 0 olamaz");
			return a * b;
		}
	}
}
