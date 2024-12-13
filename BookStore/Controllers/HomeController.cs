using BookStore.Models;
using Core.Services.BookService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookService _bookservice;
        private readonly ILogger<HomeController> _logger;

        public HomeController(BookService bookservice, ILogger<HomeController> logger)
        {
            _bookservice = bookservice;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _bookservice.GetAllBooks();
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
}
