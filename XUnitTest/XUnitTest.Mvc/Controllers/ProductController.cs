using Microsoft.AspNetCore.Mvc;
using XUnitTest.Mvc.Data.Entities;
using XUnitTest.Mvc.Repositories;

namespace XUnitTest.Mvc.Controllers
{
	public class ProductController : Controller
	{
		private readonly IRepository<Product> _repository;

		public ProductController(IRepository<Product> repository)
		{
			_repository = repository;
		}

		// GET: Products
		public async Task<IActionResult> Index()
		{
			return View(await _repository.GetAllAsync());
		}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index");
			}

			var product = await _repository.GetByIdAsync((Guid)id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		
		public IActionResult Create()
		{
			return View();
		}

	
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock,Color")] Product product)
		{
			if (ModelState.IsValid)
			{
				await _repository.CreateAsync(product);
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}

		// GET: Products/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index");
			}

			var product = await _repository.GetByIdAsync((Guid)id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, [Bind("Id,Name,Price,Stock,Color")] Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_repository.UpdateAsync(product);

				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}

		// GET: Products/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _repository.GetByIdAsync((Guid)id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var product = await _repository.GetByIdAsync(id);

			_repository.DeleteAsync(product);

			return RedirectToAction(nameof(Index));
		}

		private bool ProductExists(Guid id)
		{
			var product = _repository.GetByIdAsync(id).Result;

			if (product == null)
				return false;
			else
				return true;
		}
	}
}
