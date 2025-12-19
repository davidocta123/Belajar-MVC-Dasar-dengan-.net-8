using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Models;
using ECommerceApp.Data;

namespace ECommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            // Ambil semua produk dari database
            var products = await _context.Products.ToListAsync();
            return View(products); // kirim data ke view
        }
    }
}
