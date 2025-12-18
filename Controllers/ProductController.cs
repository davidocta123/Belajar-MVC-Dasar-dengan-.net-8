using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Models;
using ECommerceApp.Services;

namespace ECommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_productService.GetAll());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            if (product.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(product.ImageFile.FileName);
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/products",
                    fileName
                );

                using var stream = new FileStream(path, FileMode.Create);
                product.ImageFile.CopyTo(stream);
                product.ImageUrl = fileName;
            }

            _productService.Add(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            var existing = _productService.GetById(product.Id);
            if (existing == null) return NotFound();

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;

            if (product.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(product.ImageFile.FileName);
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/products",
                    fileName
                );

                using var stream = new FileStream(path, FileMode.Create);
                product.ImageFile.CopyTo(stream);
                existing.ImageUrl = fileName;
            }

            _productService.Update(existing);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
