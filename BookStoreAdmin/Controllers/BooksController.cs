using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Models;
using Core.Services.BookService;
using Core.Services.AuthorService;
using Core.DtoModels;

namespace BookStoreAdmin.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookservice;
        private readonly AuthorService _authorservice;

        public BooksController(BookService bookService, AuthorService authorService)
        {
            _bookservice = bookService;
            _authorservice = authorService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var data = await _bookservice.GetAllBooksWithAuthor();
            return View(data);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _bookservice.GetAllBooksWithAuthor(x => x.Id == id);
            var book = data.FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AuthorId"] = new SelectList(await _authorservice.GetAllAuthors() , "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookDto book)
        {
            if (ModelState.IsValid)
            {
                await _bookservice.CreateBook(book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(await _authorservice.GetAllAuthors(), "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookservice.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(await _authorservice.GetAllAuthors(), "Id", "Name", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,Img,AuthorId,Created,Updated")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookservice.UpdateBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                     throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(await _authorservice.GetAllAuthors(), "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookservice.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookservice.GetBookById(id);
            if (book != null)
            {
                await _bookservice.DeleteBook(book);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
