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
		public async Task Details_ReturnRedirectToIndexAction_WhenIdIsNull()
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

		[Fact]
		public void Create_ReturnView_WhenActionExecute()
		{
			var mock= new Mock<IRepository<Product>>();
			var controller=new ProductController(mock.Object);

			var result=  controller.Create();
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public async Task Create_ReturnView_WhenModelStateIsNotValid()
		{
			var mock = new Mock<IRepository<Product>>();
			var controller = new ProductController(mock.Object);
			controller.ModelState.AddModelError("name", "name alanı boş bırakılamaz");

			var result= await controller.Create(new Product());
			var viewResult= Assert.IsType<ViewResult>( result);
			Assert.IsType<Product>(viewResult.Model);

		}

		[Fact]
		public async Task Create_ReturnRedirectToIndexAction_WhenModelStateIsValid()
		{
			var mock= new Mock<IRepository<Product>>();
			mock.Setup(x => x.CreateAsync(new Product()));
			var controller=new ProductController(mock.Object);

			var result= await controller.Create(new Product());
			var redirect=Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", redirect.ActionName);
		
		}

		[Fact]
		public async Task Create_CreateMethodExecute_WhenModelStateIsValid()
		{
			Product product=new();
			var mock = new Mock<IRepository<Product>>();
			mock.Setup(x => x.CreateAsync(It.IsAny<Product>())).Callback<Product>(x => product=x);
			var controller=new ProductController(mock.Object);
			var result = await controller.Create(product);

			mock.Verify(x=>x.CreateAsync(It.IsAny<Product>()), Times.Once());
		}

		[Fact]

		public async Task Create_CreateMethodNotExecute_WhenModelStateIsNotValid()
		{
			var productId = Guid.NewGuid();

			var mock=new Mock<IRepository<Product>>();
			var controller= new ProductController(mock.Object);
			controller.ModelState.AddModelError("name", "Name alanı boş bırakılamaz");

			var result=await controller.Create(new Product());
			mock.Verify(x => x.CreateAsync(It.IsAny<Product>()), Times.Never());
		}

		[Fact]
		public async Task Edit_ReturnRedirectoIndexAction_WhenIdIsNull()
		{
			var mock = new Mock<IRepository<Product>>();
			var controller = new ProductController(mock.Object);

			var result=await controller.Edit(null);
			var redirect=Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", redirect.ActionName);
		}

		[Fact]
		public async Task Edit_ReturnNotFound_WhenIdIsNotValid()
		{
			Product product = null;
			var productId = Guid.NewGuid();

			var mock= new Mock<IRepository<Product>>();
			mock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
			var controller=new ProductController(mock.Object);

			var result = await controller.Edit(productId);
			var redirect = Assert.IsType<NotFoundResult>(result);
			Assert.Equal(404,redirect.StatusCode);
		}

		[Fact]
		public async Task Edit_ReturnProduct_WhenIdIsValid()
		{
			var productId= Guid.NewGuid();
			var mock= new Mock<IRepository<Product>>();
			mock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Product() { Id= productId });
			var controller=new ProductController(mock.Object);

			var result= await controller.Edit(productId);
			var viewResult = Assert.IsType<ViewResult>(result);
		    var resultProduct=Assert.IsAssignableFrom<Product>(viewResult.Model);
			Assert.Equal<Guid>(productId, resultProduct.Id);
		}

		[Fact]
		public void Edit_ReturnNotFound_WhenIdIsNotEqualProductId()
		{
			var guidId = Guid.NewGuid();
			var productId = new Product() { Id = Guid.NewGuid() };

			var mock =new Mock<IRepository<Product>>();
			var controller=new ProductController(mock.Object);

			var result = controller.Edit(guidId, productId);
			var redirect=Assert.IsType<NotFoundResult>(result);

		}

		[Fact]
		public void Edit_ReturnView_WhenModelStateIsNotValid()
		{
			var guidId = Guid.NewGuid();
			var productId = new Product() { Id = guidId };

			var mock=new Mock<IRepository<Product>>();
			var controller= new ProductController(mock.Object);
			controller.ModelState.AddModelError("Name", "Name alanı boş bırakılamaz");

			var result= controller.Edit(guidId,productId);
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsType<Product>(viewResult.Model);
		}

		[Fact]
		public void Edit_ReturnRedirectToIndexAction_WhenModelStateIsValid()
		{
			var guidId = Guid.NewGuid();
			var productId = new Product() { Id = guidId };

			var mock= new Mock<IRepository<Product>>();
			var controller = new ProductController(mock.Object);

			var result=controller.Edit(guidId,productId);
			var redirect= Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", redirect.ActionName);
		}

		[Fact]
		public void Edit_UpdateMethodExecute_WhenModelStateIsValid()
		{
			var guidId = Guid.NewGuid();
			var productId = new Product() { Id = guidId };

			var mock = new Mock<IRepository<Product>>();
			mock.Setup(x => x.UpdateAsync(It.IsAny<Product>()));
			var controller=new ProductController(mock.Object);

			var result= controller.Edit(guidId,productId);
			mock.Verify(x=>x.UpdateAsync(It.IsAny<Product>()),Times.Once());
		}
	
	}
}
