using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using XUnitTest.Mvc.Controllers;
using XUnitTest.Mvc.Data.Entities;
using XUnitTest.Mvc.Repositories;

namespace XUnitTest.Test.XUnitTest.Mvc.Test
{
	public class ProductControllerTest
	{
		[Fact]
		public async Task Index_ReturnView_WhenActionExecute()
		{
			var mock = new Mock<IRepository<Product>>();
			var controller= new ProductController(mock.Object);
			var result =await controller.Index();
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public async Task Index_ReturnProductList_WhenActionExecute()
		{
			var productList = new List<Product>() { new Product() };

			var mock = new Mock<IRepository<Product>>();
			mock.Setup(x=>x.GetAllAsync()).ReturnsAsync(productList);
			var controller= new ProductController(mock.Object);

			var result=await controller.Index();
			var viewResult=Assert.IsType<ViewResult>(result);
			var products=Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
			Assert.Equal(1, products.Count());
		}
	}
}
