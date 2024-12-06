using DataAccess.Models;
using DataAccess.Repositories.AuthorRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.AuthorService
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository) 
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAll();
        }

        public async Task CreateAuthor(Author author)
        {
            await _authorRepository.Create(author);
        }
        public async Task GetAuthorById(int id)
        {
            await _authorRepository.GetById(id);
        }
    }
}
