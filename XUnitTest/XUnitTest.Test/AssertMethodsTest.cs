using Xunit;

namespace XUnitTest.Test
{


	public class AssertMethodsTest
	{
		[Fact]
		public void EqualsTest()
		{
			Assert.Equal("Pinar", "Pinar");
			Assert.NotEqual("Pinar", "Emre");
			Assert.Equal<int>(2, 5);
			Assert.NotEqual<int>(2, 5);
		}

		[Fact]
		public void ContainsTest()
		{
			Assert.Contains("Pinar", "PinarAliogullariKaya");
			Assert.NotEqual("Pinar", "Aliogullari");
		}

		[Fact]
		public void TrueFalseTest()
		{
			Assert.True("".GetType()==typeof(string));
			Assert.False("".GetType() == typeof(int));
		}

		[Fact]
		public void MatchTest()
		{
			var regEx = "^cat";
			Assert.Matches(regEx,"cat");
			Assert.DoesNotMatch(regEx,"Slyvester");
		}

		[Fact]
		public void StartEndWith()
		{
			Assert.StartsWith("One","OneDay");
			Assert.EndsWith("Day", "OneDay");
		}

		[Fact]
		public void EmptyTest()
		{
			Assert.Empty(new List<string>(){ });
			Assert.NotEmpty(new List<string>() { "Pinar"});
		}

		[Fact]
		public void RangeTest()
		{
			Assert.InRange(10,2,11);
			Assert.NotInRange(10,2,8);
		}

		[Fact]
		public void SingleTest()
		{
			Assert.Single(new List<string>() { "Pinar"});
			Assert.Single(new List<string>() { "Pinar","Emre"});
		}

		[Fact]
		public void IsTypeTest()
		{
			Assert.IsType<string>("Pinar");
			Assert.IsNotType<int>("Pinar");
		}
		[Fact]
		public void IsAssignableFromTest()
		{
			Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>());
			Assert.IsAssignableFrom<object>(2);
			Assert.IsAssignableFrom<object>("Pinar");
		}

		[Fact]
		public void NullTest()
		{
			string value = null;
			Assert.Null(value);
			Assert.NotNull(value);
		}
	}
}
