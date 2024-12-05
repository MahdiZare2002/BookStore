using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.AuthorRepo
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookDbContext _context;
        public AuthorRepository(BookDbContext context)
        {
            _context = context;
        }
        public async Task Create(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Author author)
        {
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var data = await _context.Author.ToListAsync();
            return data;
        }

        public async Task<Author> GetById(int id)
        {
            var data = await _context.Author.FindAsync(id);
            return data;

        }

        public async Task Update(Author author)
        {
            _context.Author.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}
