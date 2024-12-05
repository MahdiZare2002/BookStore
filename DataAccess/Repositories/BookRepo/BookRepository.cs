using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.BookRepo
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;
        public BookRepository(BookDbContext context) 
        {
            _context = context;
        }

        public async Task Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Book> GetAll(Expression<Func<Book, bool>> where = null)
        {
            var data = _context.Books.AsQueryable();
            if (where != null)
            {
                data.Where(where);
            }
            return data;
        }

        public async Task<Book> GetById(int id)
        {
            var data = await _context.Books.FindAsync(id);
            return data;
        }

        public async Task Update(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
