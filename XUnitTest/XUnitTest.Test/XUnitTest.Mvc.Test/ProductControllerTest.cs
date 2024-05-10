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
			var data=Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
			Assert.Equal(1, data.Count());
		}

		[Fact]
		public async Task Details_ReturnRedirectToAction_WhenIdIsNull()
		{
			var mock= new Mock<IRepository<Product>>();
			var controller = new ProductController(mock.Object);

			var result = await controller.Details(null);
			var redirect=Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index",redirect.ActionName);
		}

		[Fact]
		public async Task Details_ReturnNotFound_WhenIdIsNotValid()
		{
			var productId=Guid.NewGuid();
			Product product = null;

			var mock= new Mock<IRepository<Product>>();
			mock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
			var controller = new ProductController(mock.Object);

			var result= await controller.Details(productId);
			var redirect=Assert.IsType<NotFoundResult>(result);
			Assert.Equal<int>(404, redirect.StatusCode);
		}

		[Fact]
		public async Task Details_ReturnProduct_WhenIdIsValid()
		{
			var productId=Guid.NewGuid();

			var mock= new Mock<IRepository<Product>>();
			mock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(new Product() { Id=productId});
			var controller= new ProductController(mock.Object);

			var result= await controller.Details(productId);
			var viewResult= Assert.IsType<ViewResult>(result);
			var data=Assert.IsAssignableFrom<Product>(viewResult.Model);
			Assert.Equal<Guid>(productId, data.Id);
		}
	}
}
