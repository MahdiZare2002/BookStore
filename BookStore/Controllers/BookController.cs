using Core.DtoModels;
using Core.Services.BookService;
using DataAccess.Repositories.BookRepo;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly BookRepository _bookRepository;

        public BookController(BookService bookService, BookRepository bookRepository)
        {
            _bookService = bookService;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var data = await _bookService.GetBookById(id);
            return View(data);
        }

        public async Task<IActionResult> GetBooks(int page = 1, int pageSize = 5, string search = null)
        {
            var data = await _bookService.GetBooksPaginated(page, pageSize, search);
            ViewBag.search = search;
            return View(data);
        }
    }
}
