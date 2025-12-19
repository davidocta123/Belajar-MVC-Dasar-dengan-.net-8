using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Data;
using ECommerceApp.Models;

namespace ECommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index(int userId = 1) // contoh user statis
        {
            var cart = await _context.CartItems
                                     .Where(c => c.UserId == userId)
                                     .ToListAsync();
            return View(cart);
        }
        // POST: Add product to cart
        [HttpPost]
        public async Task<IActionResult> Add(int productId, int userId = 1)
        {
            var item = await _context.CartItems
                                     .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

            if (item == null)
            {
                var product = await _context.Products.FindAsync(productId);
                if (product == null) return NotFound();

                var cartItem = new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Description = product.Description,
                    Quantity = 1,
                    UserId = userId
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                item.Quantity++;
                _context.CartItems.Update(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // Increase quantity
        public async Task<IActionResult> Increase(int id, int userId = 1)
        {
            var item = await _context.CartItems
                                     .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (item != null)
            {
                item.Quantity++;
                _context.CartItems.Update(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        // Decrease quantity
        public async Task<IActionResult> Decrease(int id, int userId = 1)
        {
            var item = await _context.CartItems
                                     .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                    _context.CartItems.Remove(item);
                else
                    _context.CartItems.Update(item);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
