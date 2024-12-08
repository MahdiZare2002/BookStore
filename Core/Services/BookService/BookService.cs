using DataAccess.Models;
using DataAccess.Repositories.BookRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.BookService
{
    public class BookService
    {
        private readonly IBookRepository _bookrepository;
        public BookService(IBookRepository bookrepository)
        {
            _bookrepository = bookrepository;
        }
        public async Task<IEnumerable<Book>> GetAllBooks() 
        { 
            return await _bookrepository.GetAll().ToListAsync();
        } 
        public async Task<IEnumerable<Book>> GetAllBooksWithAuthor(Expression<Func<Book, bool>> where = null)
        {
            return await _bookrepository.GetAll(where).Include(x => x.Author).ToListAsync();
        }
        public async Task CreateBook(Book book)
        {
            await _bookrepository.Create(book);
        }
        public async Task UpdateBook(Book book)
        {
            await _bookrepository.Update(book);
        }
        public async Task DeleteBook(Book book)
        {
            await _bookrepository.Delete(book);
        }
        public async Task<Book> GetBookById(int id)
        {
            return await _bookrepository.GetById(id);
        }
    }
}
