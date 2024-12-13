using Core.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var data = await _bookService.GetBookById(id);
            return View(data);
        }
    }
}
