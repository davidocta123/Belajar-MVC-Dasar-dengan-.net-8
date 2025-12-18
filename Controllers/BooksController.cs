using Microsoft.AspNetCore.Mvc;
using TugasWepAppBook.Models; // ← INI YANG KURANG

namespace TugasWepAppBook.Controllers; // ← HARUS ADA Controllers

public class BooksController : Controller
{
    private static readonly List<Book> books = new()
    {
        new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", PublicationYear = 2008 },
        new Book { Id = 2, Title = "Design Patterns", Author = "Erich Gamma", PublicationYear = 1994 },
        new Book { Id = 3, Title = "Refactoring", Author = "Martin Fowler", PublicationYear = 1999 }
    };

    public IActionResult Index()
    {
        return View(books);
    }

    public IActionResult Detail(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound();

        return View(book);
    }
}
