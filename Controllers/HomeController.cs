using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TugasWepAppBook.Models;

namespace TugasWepAppBook.Controllers;

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
}
