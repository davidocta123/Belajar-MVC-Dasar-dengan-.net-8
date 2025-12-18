using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Services;

namespace ECommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            return View(products); // ✅ kirim data ke view
        }
    }
}
