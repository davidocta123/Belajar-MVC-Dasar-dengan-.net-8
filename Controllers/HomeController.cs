using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using WepAppMVC.Models;

namespace WepAppMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Message"] = "Selamat datang di aplikasi web .NET 8 Anda!";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public IActionResult ProductDetail()
    {
        var product = new Product
        {
            Id = 1,
            Name = "Laptop Gaming",
            Price = 15000000.00m
        };
        return View(product);
    }
}
