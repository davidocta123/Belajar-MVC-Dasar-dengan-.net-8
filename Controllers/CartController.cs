using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Models;
using ECommerceApp.Services;
using ECommerceApp.Extensions;

namespace ECommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private const string CART_KEY = "CART";

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY)
                       ?? new List<CartItem>();

            return View(cart);
        }

        [HttpPost]
        public IActionResult Add(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY)
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Description = product.Description,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }

            HttpContext.Session.SetObject(CART_KEY, cart);
            return RedirectToAction("Index", "Cart", new { area = "Customer" });
        }

        public IActionResult Increase(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY)
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
                item.Quantity++;

            HttpContext.Session.SetObject(CART_KEY, cart);
            return RedirectToAction("Index", "Cart", new { area = "Customer" });
        }

        public IActionResult Decrease(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY)
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                    cart.Remove(item);
            }

            HttpContext.Session.SetObject(CART_KEY, cart);
            return RedirectToAction("Index", "Cart", new { area = "Customer" });
        }
    }
}
